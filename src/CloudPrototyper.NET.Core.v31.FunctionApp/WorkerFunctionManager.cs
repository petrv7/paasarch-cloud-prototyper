using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.Interface;
using CloudPrototyper.Interface.Constants;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.Interface.Prototyper;
using CloudPrototyper.Model;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.Model.Operations;
using CloudPrototyper.Model.Resources;
using CloudPrototyper.NET.Core.v31.Functions.Generators;
using CloudPrototyper.NET.Core.v31.Functions.Generators.Functions;
using CloudPrototyper.NET.Core.v31.Functions.Model;
using CloudPrototyper.NET.Framework.v462.Common.Factories;
using CloudPrototyper.NET.Framework.v462.Common.Generators.SolutionGenerators;
using CloudPrototyper.NET.Interface.Constants;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Standard.v20.CosmosDb.Model;
using CloudPrototyper.NET.Standard.v20.EventHub.Model;
using Action = CloudPrototyper.Model.Applications.Action;
using ProjectFactory = CloudPrototyper.NET.Core.v31.Common.Factories.ProjectFactory;

namespace CloudPrototyper.NET.Core.v31.FunctionApp
{
    public class WorkerFunctionManager : GeneratorManager<WorkerApplication>, IServerless
    {
        /// <summary>
        /// Used to register and resolve all IGenerableFiles.
        /// </summary>
        public WindsorContainer Container { get; set; } = new WindsorContainer();

        /// <summary>
        /// Application files.
        /// </summary>
        /// <returns>Application files to be generated.</returns>
        public override IGenerable GetGenerable()
        {
            return ApplicationGenerator;
        }

        /// <summary>
        /// Lists of resources required by managed application.
        /// </summary>
        /// <returns>List of resources required by managed application.</returns>
        public override IList<Resource> GetRequiredResources()
        {
            var res = Utils.FindAllInstances<Resource>(Prototype)
                    .Where(y => Utils.FindAllInstances<Operation>(ApplicationGenerator.Model)
                        .SelectMany(x => x.GetReferencedResources()).Select(z => z.Name).Contains(y.Name)).ToList();
            res.AddRange(Utils.FindAllInstances<AzureEventHubNamespace>(Prototype).Where(n => Utils.FindAllInstances<AzureEventHub>(res).Select(h => h.WithNamespace).Contains(n.Name)));
            res.Add(Utils.FindAllInstances<AzureFunctionApp>(Prototype).Single(x => x.WithApplication == ApplicationGenerator.Model.Name));
            return res;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="application">Application to be managed.</param>
        /// <param name="prototype">Whole prototype with entities and resources.</param>
        /// <param name="configProvider">Configuration provider.</param>
        public WorkerFunctionManager(WorkerApplication application, Prototype prototype, IConfigProvider configProvider) : base(new ApplicationGenerator<WorkerApplication>(application, configProvider.GetValue("OutputFolderPath") + "\\" + application.Name), prototype, "DotNetCore31", configProvider.GetValue("OutputFolderPath") + "\\" + application.Name, configProvider.GetValue("OutputFolderPath") + "\\" + application.Name
            , configProvider)
        {
            Container.Kernel.Resolver.AddSubResolver(new CollectionResolver(Container.Kernel, true));

            var operations = Utils.FindAllInstances<Operation>(application);
            var actions = Utils.FindAllInstances<Action>(application);

            ProjectFactory.RegisterOperations(NamingConstants.WorkerName, actions, Container);
            ProjectFactory.RegisterEntities(NamingConstants.WorkerName, application, prototype, Container);
            ProjectFactory.RegisterResources(NamingConstants.WorkerName, application, prototype, operations, Container);

            RegisterFunctionComponents(application, prototype);
            var handlers = Container.Kernel.GetAssignableHandlers(typeof(IGenerableFile));

            var functionProjectFiles = ProjectFactory.ResolveHandlers(handlers, Container, NamingConstants.WorkerName);

            InitFunctionProject(functionProjectFiles, Container, prototype, application);

            RegisterSolutionFiles(application);

            var allFiles = ProjectFactory.ResolveHandlers(Container.Kernel.GetAssignableHandlers(typeof(IGenerableFile)), Container);

            ApplicationGenerator.Files.AddRange(allFiles);
        }

        private void RegisterSolutionFiles(WorkerApplication application)
        {
            Container.Register(
               Component.For<SolutionFileGenerator>().ImplementedBy<SolutionFileGenerator>().LifestyleSingleton().DependsOn(Dependency.OnValue("solutionName", application.Name))
           );
        }

        private void RegisterFunctionComponents(WorkerApplication application, Prototype prototype)
        {
            Container.Register(
                Component.For<StartupGenerator>().ImplementedBy<StartupGenerator>()
                    .DependsOn(Dependency.OnValue("projectName", NamingConstants.WorkerName))
            );

            var queueNames = application.Actions.Select(a => a.Trigger).OfType<MessageReceivedTrigger>().Select(t => t.QueueName);
            var cosmosTriggers = application.Actions.Select(a => a.Trigger).OfType<AzureCosmosDbTrigger>();
            var containerNames = cosmosTriggers.Select(t => t.ContainerName).ToList();

            var buses = Utils.FindAllInstances<AzureServiceBusQueue>(prototype).Where(b => queueNames.Contains(b.Name));
            var hubs = Utils.FindAllInstances<AzureEventHub>(prototype).Where(h => queueNames.Contains(h.Name));
            var containers = Utils.FindAllInstances<AzureCosmosDbContainer>(prototype)
                .Where(c => containerNames.Contains(c.Name));

            foreach (var bus in buses)
            {
                var busActions = application.Actions.Where(a => a.Trigger is MessageReceivedTrigger t && t.QueueName == bus.Name).ToList();

                Container.Register(
                    Component.For<ServiceBusFunctionGenerator>().ImplementedBy<ServiceBusFunctionGenerator > ().LifestyleSingleton().DependsOn(Dependency.OnValue("projectName", NamingConstants.WorkerName)).DependsOn(Dependency.OnValue(
                        "modelParameters", busActions)).DependsOn(Dependency.OnValue("azureServiceBusQueue", bus)).Named(bus.Name + typeof(ServiceBusFunctionGenerator))
                    );
            }

            foreach (var hub in hubs)
            {
                var hubActions = application.Actions.Where(a => a.Trigger is MessageReceivedTrigger t && t.QueueName == hub.Name).ToList();

                Container.Register(
                    Component.For<EventHubFunctionGenerator>().ImplementedBy<EventHubFunctionGenerator>().LifestyleSingleton().DependsOn(Dependency.OnValue("projectName", NamingConstants.WorkerName)).DependsOn(Dependency.OnValue(
                        "modelParameters", hubActions)).DependsOn(Dependency.OnValue("azureEventHub", hub)).Named(hub.Name + typeof(EventHubFunctionGenerator))
                    );
            }

            foreach (var container in containers)
            {
                var action = application.Actions.Single(a => a.Trigger is AzureCosmosDbTrigger t && t.ContainerName == container.Name);

                Container.Register(Component.For<CosmosDbFunctionGenerator>()
                    .ImplementedBy<CosmosDbFunctionGenerator>().LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", NamingConstants.WorkerName))
                    .DependsOn(Dependency.OnValue("actionName", action.Name))
                    .DependsOn(Dependency.OnValue("container", container))
                    .DependsOn(Dependency.OnValue("trigger", cosmosTriggers.Single(t => t.ContainerName == container.Name))));
            }
        }

        private void InitFunctionProject(List<IGenerableFile> includes, WindsorContainer container, Prototype prototype, WorkerApplication application)
        {
            var packages = new List<PackageConfigInfo>();

            foreach (var include in includes.OfType<CodeGeneratorBase>())
            {
                packages.AddRange(include.GetNugetPackages());
            }
            var contents = new List<ContentInfo>();
            foreach (var include in includes.OfType<CodeGeneratorBase>())
            {
                contents.AddRange(include.GetContents(NamingConstants.WorkerName));
            }
            ApplicationGenerator.Contents.AddRange(contents);
            packages = packages.Distinct().ToList();

            var queueNames = application.Actions.Select(a => a.Trigger).OfType<MessageReceivedTrigger>().Select(t => t.QueueName);

            var buses = Utils.FindAllInstances<AzureServiceBusQueue>(prototype).Where(b => queueNames.Contains(b.Name)).ToList();
            var hubs = Utils.FindAllInstances<AzureEventHub>(prototype).Where(h => queueNames.Contains(h.Name)).ToList();

            ProjectFactory.RegisterSolutionLayer(NamingConstants.WorkerName, ProjectType.FunctionApp, packages, ApplicationGenerator.Files, includes, contents, new List<AssemblyBase>(), container, buses, hubs);
        }

        public override void Dispose()
        {
            Container?.Dispose();
        }
    }
}
