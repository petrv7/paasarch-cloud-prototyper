using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
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
using CloudPrototyper.NET.Standard.v20.EventHub.Model;
using Action = CloudPrototyper.Model.Applications.Action;
using ProjectFactory = CloudPrototyper.NET.Core.v31.Common.Factories.ProjectFactory;

namespace CloudPrototyper.NET.Core.v31.FunctionApp
{
    public class HttpFunctionManager : GeneratorManager<RestApiApplication>, IServerless
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
            res.Add(Utils.FindAllInstances<AzureFunctionApp>(Prototype).Single(x => x.WithApplication == ApplicationGenerator.Model.Name));
            res.AddRange(Utils.FindAllInstances<AzureEventHubNamespace>(Prototype).Where(n => Utils.FindAllInstances<AzureEventHub>(res).Select(h => h.WithNamespace).Contains(n.Name)));

            return res;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="application">Application to be managed.</param>
        /// <param name="prototype">Whole prototype with entities and resources.</param>
        /// <param name="configProvider">Configuration provider.</param>
        public HttpFunctionManager(RestApiApplication application, Prototype prototype, IConfigProvider configProvider) : base(new ApplicationGenerator<RestApiApplication>(application, configProvider.GetValue("OutputFolderPath") + "\\" + application.Name), prototype, "DotNetCore31", configProvider.GetValue("OutputFolderPath") + "\\" + application.Name, configProvider.GetValue("OutputFolderPath") + "\\" + application.Name
            , configProvider)
        {
            Container.Kernel.Resolver.AddSubResolver(new CollectionResolver(Container.Kernel, true));

            var operations = Utils.FindAllInstances<Operation>(application);
            var actions = Utils.FindAllInstances<Action>(application);

            RegisterDataLayer(application, prototype, operations, Container);
            var dataLayerHandlers = Container.Kernel.GetAssignableHandlers(typeof(IGenerableFile));

            RegisterBusinessLayer(actions, Container);
            var businessLayerHandlers = Container.Kernel.GetAssignableHandlers(typeof(IGenerableFile)).Except(dataLayerHandlers).ToArray();

            RegisterFunctionLayer(actions);
            var functionLayerHandlers = Container.Kernel.GetAssignableHandlers(typeof(IGenerableFile)).Except(dataLayerHandlers).Except(businessLayerHandlers).ToArray();

            RegisterSolutionItems(application);

            var dataLayerFiles = ProjectFactory.ResolveHandlers(dataLayerHandlers, Container, NamingConstants.DataLayerProjectName);
            var businessLayerFiles = ProjectFactory.ResolveHandlers(businessLayerHandlers, Container, NamingConstants.BusinessLayerProjectName);
            var functionLayerFiles = ProjectFactory.ResolveHandlers(functionLayerHandlers, Container, NamingConstants.FunctionLayerProjectName);

            InitDataLayer(dataLayerFiles, Container);
            InitBusinessLayer(businessLayerFiles, Container);
            InitFunctionLayer(functionLayerFiles, Container);

            var allFiles = ProjectFactory.ResolveHandlers(Container.Kernel.GetAssignableHandlers(typeof(IGenerableFile)), Container);
            ApplicationGenerator.Files.AddRange(allFiles);
        }

        private static void RegisterBusinessLayer(List<Action> actions, WindsorContainer container)
        {
            ProjectFactory.RegisterOperations(NamingConstants.BusinessLayerProjectName, actions, container);
        }

        private static void RegisterDataLayer(RestApiApplication application, Prototype prototype, List<Operation> operations, WindsorContainer container)
        {
            ProjectFactory.RegisterEntities(NamingConstants.DataLayerProjectName, application, prototype, container);
            ProjectFactory.RegisterResources(NamingConstants.DataLayerProjectName, application, prototype, operations, container);
        }

        private void RegisterFunctionLayer(List<Action> actions)
        {
            Container.Register(
                Component.For<StartupGenerator>().ImplementedBy<StartupGenerator>()
                    .DependsOn(Dependency.OnValue("projectName", NamingConstants.FunctionLayerProjectName))
            );

            foreach (var action in actions)
            {
                Container.Register(Component.For<HttpFunctionGenerator>()
                    .ImplementedBy<HttpFunctionGenerator>()
                    .DependsOn(Dependency.OnValue("projectName", NamingConstants.FunctionLayerProjectName))
                    .DependsOn(Dependency.OnValue("actionName", action.Name))
                    .LifestyleSingleton()
                    .Named(action.Name + typeof(HttpFunctionGenerator)));
            }
        }

        private void RegisterSolutionItems(RestApiApplication application)
        {
            Container.Register(
              Component.For<RestApiApplication>().Instance(application)
              );

            foreach (var action in application.Actions)
            {
                Container.Register(
                    Component.For<CallableAction>().Instance(action).Named(action.Name + nameof(CallableAction))
                    );
            }
            Container.Register(
                    Component.For<SolutionFileGenerator>().ImplementedBy<SolutionFileGenerator>().LifestyleSingleton().DependsOn(Dependency.OnValue("solutionName", application.Name))
                );
        }


        private void InitFunctionLayer(List<IGenerableFile> includes, WindsorContainer container)
        {
            var packages = new List<PackageConfigInfo>();

            foreach (var include in includes.OfType<CodeGeneratorBase>())
            {
                packages.AddRange(include.GetNugetPackages());
            }

            var contents = new List<ContentInfo>();
            foreach (var include in includes.OfType<CodeGeneratorBase>())
            {
                contents.AddRange(include.GetContents(NamingConstants.FunctionLayerProjectName));
            }

            ApplicationGenerator.Contents.AddRange(contents);
            packages = packages.Distinct().ToList();
            ProjectFactory.RegisterSolutionLayer(NamingConstants.FunctionLayerProjectName, ProjectType.FunctionApp, packages, ApplicationGenerator.Files, includes, contents, new List<AssemblyBase> { container.Resolve<AssemblyBase>(NamingConstants.DataLayerProjectName), container.Resolve<AssemblyBase>(NamingConstants.BusinessLayerProjectName) }, container);
        }

        private void InitBusinessLayer(List<IGenerableFile> includes, WindsorContainer container)
        {
            var packages = new List<PackageConfigInfo>();

            foreach (var include in includes.OfType<CodeGeneratorBase>())
            {
                packages.AddRange(include.GetNugetPackages());
            }

            var contents = new List<ContentInfo>();
            foreach (var include in includes.OfType<CodeGeneratorBase>())
            {
                contents.AddRange(include.GetContents(NamingConstants.BusinessLayerProjectName));
            }

            ApplicationGenerator.Contents.AddRange(contents);

            packages = packages.Distinct().ToList();
            ProjectFactory.RegisterSolutionLayer(NamingConstants.BusinessLayerProjectName, ProjectType.Library, packages, ApplicationGenerator.Files, includes, contents, new List<AssemblyBase>
            { container.Resolve<AssemblyBase>(NamingConstants.DataLayerProjectName) }, container);
        }

        private void InitDataLayer(List<IGenerableFile> includes, WindsorContainer container)
        {
            var packages = new List<PackageConfigInfo>();

            foreach (var include in includes.OfType<CodeGeneratorBase>())
            {
                packages.AddRange(include.GetNugetPackages());
            }


            var contents = new List<ContentInfo>();
            foreach (var include in includes.OfType<CodeGeneratorBase>())
            {
                contents.AddRange(include.GetContents(NamingConstants.DataLayerProjectName));
            }

            ApplicationGenerator.Contents.AddRange(contents);
            packages = packages.Distinct().ToList();
            ProjectFactory.RegisterSolutionLayer(NamingConstants.DataLayerProjectName, ProjectType.Library, packages, ApplicationGenerator.Files, includes, contents, new List<AssemblyBase>(), container);
        }

        public override void Dispose()
        {
            Container?.Dispose();
        }
    }
}
