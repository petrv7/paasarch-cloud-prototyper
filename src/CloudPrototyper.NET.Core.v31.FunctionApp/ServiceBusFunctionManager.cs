using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using CloudPrototyper.Azure.Resources;
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
using CloudPrototyper.NET.Framework.v462.Common.Factories;
using CloudPrototyper.NET.Framework.v462.Common.Generators.SolutionGenerators;
using CloudPrototyper.NET.Framework.v462.EventHub.Model;
using CloudPrototyper.NET.Interface.Constants;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using Action = CloudPrototyper.Model.Applications.Action;
using ProjectFactory = CloudPrototyper.NET.Core.v31.Common.Factories.ProjectFactory;

namespace CloudPrototyper.NET.Core.v31.Functions
{
    public class ServiceBusFunctionManager : GeneratorManager<WorkerApplication>, IServerless
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
        public ServiceBusFunctionManager(WorkerApplication application, Prototype prototype, IConfigProvider configProvider) : base(new ApplicationGenerator<WorkerApplication>(application, configProvider.GetValue("OutputFolderPath") + "\\" + application.Name), prototype, "DotNetCore31", configProvider.GetValue("OutputFolderPath") + "\\" + application.Name, configProvider.GetValue("OutputFolderPath") + "\\" + application.Name
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

            List<IGenerableFile> consoleProjectFiles = ProjectFactory.ResolveHandlers(handlers, Container, NamingConstants.WorkerName);

            InitFunctionProject(consoleProjectFiles, Container, prototype);

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
                Component.For<CastleWindsorJobActivatorGenerator>().ImplementedBy<CastleWindsorJobActivatorGenerator>()
                    .DependsOn(Dependency.OnValue("projectName", NamingConstants.WorkerName))
            );
            Container.Register(
                Component.For<StartupGenerator>().ImplementedBy<StartupGenerator>()
                    .DependsOn(Dependency.OnValue("projectName", NamingConstants.WorkerName))
            );

            var buses = Utils.FindAllInstances<AzureServiceBusQueue>(prototype);

            foreach (var bus in buses)
            {
                Container.Register(
                    Component.For<ServiceBusFunctionGenerator>().ImplementedBy<ServiceBusFunctionGenerator>().LifestyleSingleton().DependsOn(Dependency.OnValue("projectName", NamingConstants.WorkerName)).DependsOn(Dependency.OnValue(
                        "modelParameters", application.Actions)).DependsOn(Dependency.OnValue("azureServiceBusQueue", bus)).Named(bus.Name + typeof(ServiceBusFunctionGenerator))
                    );
            }
        }

        private void InitFunctionProject(List<IGenerableFile> includes, WindsorContainer container, Prototype prototype)
        {
            var packages = new List<PackageConfigInfo>();

            foreach (CodeGeneratorBase include in includes.OfType<CodeGeneratorBase>())
            {
                packages.AddRange(include.GetNugetPackages());
            }
            var contents = new List<ContentInfo>();
            foreach (CodeGeneratorBase include in includes.OfType<CodeGeneratorBase>())
            {
                contents.AddRange(include.GetContents(NamingConstants.WorkerName));
            }
            ApplicationGenerator.Contents.AddRange(contents);
            packages = packages.Distinct().ToList();

            var buses = Utils.FindAllInstances<AzureServiceBusQueue>(prototype);

            ProjectFactory.RegisterSolutionLayer(NamingConstants.WorkerName, ProjectType.FunctionApp, packages, ApplicationGenerator.Files, includes, contents, new List<AssemblyBase>(), container, buses);
        }

        public override void Dispose()
        {
            Container?.Dispose();
        }
    }
}
