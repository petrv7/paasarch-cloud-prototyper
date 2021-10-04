using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel;
using Castle.Windsor;
using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.Interface;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.Model;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.Model.Operations;
using CloudPrototyper.Model.Resources;
using CloudPrototyper.Model.Resources.Storage;
using CloudPrototyper.NET.Core.v31.Common.Generators.SolutionGenerators.AssemblyFiles;
using CloudPrototyper.NET.Core.v31.Functions.Generators;
using CloudPrototyper.NET.Core.v31.Functions.Templates;
using CloudPrototyper.NET.Framework.v462.Common.Factories;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Standard.v20.EventHub.Model;
using Component = Castle.MicroKernel.Registration.Component;

namespace CloudPrototyper.NET.Core.v31.Common.Factories
{
    public static class ProjectFactory
    {
        /// <summary>
        /// Registers all future entities and related generators into the container.
        /// </summary>
        /// <param name="application">Result application.</param>
        /// <param name="prototype">Prototype model.</param>
        /// <param name="projectName">Name of result project containing this entities, used for namespace and folder.</param>
        /// <param name="container">Container storing all IGenerableFile instances.</param>
        public static void RegisterEntities(string projectName, Application application, Prototype prototype, WindsorContainer container)
        {
            EntityFactory.RegisterEntities(prototype, projectName, container);
        }

        /// <summary>
        /// Registers all resources file generators.
        /// </summary>        
        /// <param name="projectName">Name of result project containing this entities, used for namespace and folder.</param>
        /// <param name="application">Result application.</param>        
        /// <param name="prototype">Prototype model.</param>
        /// <param name="operations">List of operations that may use resources.</param>
        /// <param name="container">Container storing all IGenerableFile instances.</param>
        public static void RegisterResources(string projectName, Application application, Prototype prototype, List<Operation> operations, WindsorContainer container)
        {
            var storages = Utils.FindAllInstances<Resource>(prototype)
                .Where(x => operations.SelectMany(y => y.GetReferencedResources().Select(z => z.Name)).Contains(x.Name)).ToList();

            ResourceFactory.RegisterResources(storages, prototype, projectName, container);
        }

        /// <summary>
        /// Registers actions and operations to provided container.
        /// </summary>
        /// <param name="projectName">Project name used as namespace and folder.</param>
        /// <param name="actions">List of actions withtin the application.</param>
        /// <param name="container"></param>
        public static void RegisterOperations(string projectName, List<Model.Applications.Action> actions, WindsorContainer container)
        {
            OperationFactory.RegisterOperations(projectName, actions, container);
        }

        /// <summary>
        /// Handles function app project specific matters.
        /// </summary>
        /// <param name="baseNamespace">Base namespace in project.</param>
        /// <param name="name">Name of project.</param>
        /// <param name="includes">Files to be included.</param>
        /// <param name="contents">Content to be included.</param>
        /// <param name="files">All files in project.</param>
        /// <param name="nugets">Nugets to be referenced.</param>
        /// <param name="imports">Projects to be referenced.</param>
        /// <returns></returns>
        public static AssemblyBase MakeFunctionAppProject(string baseNamespace, string name, List<IGenerableFile> includes, List<ContentInfo> contents, List<IGenerableFile> files, List<PackageConfigInfo> nugets, List<AssemblyBase> imports, List<AzureServiceBusQueue> queues, List<AzureEventHub> hubs)
        {
            AssemblyBase functionAppProject = new FunctionAssemblyFileGenerator(new List<IGenerableFile>(), new AssemblyInfo { Name = name, ProjectFileRelativePath = name });

            functionAppProject.AssemblyInfo.Contents.AddRange(contents);

            HostJsonGenerator hostJson = new HostJsonGenerator(new GenerationInfo("host.json", functionAppProject.GenerationInfo.RelativePathFolder, new HostJsonTemplate(), true));
            LocalSettingsJsonGenerator localSettingsJson = new LocalSettingsJsonGenerator(new GenerationInfo("local.settings.json", functionAppProject.GenerationInfo.RelativePathFolder, new LocalSettingsJsonTemplate(), true), queues, hubs);
            
            files.Add(functionAppProject);
            files.Add(hostJson);
            files.Add(localSettingsJson);

            functionAppProject.AssemblyInfo.AssemblyImports.AddRange(imports);
            functionAppProject.AssemblyInfo.Packages.AddRange(nugets);

            return functionAppProject;
        }

        /// <summary>
        /// Handles library project specific matters.
        /// </summary>
        /// <param name="baseNamespace">Base namespace in project.</param>
        /// <param name="name">Name of project.</param>
        /// <param name="includes">Files to be included.</param>
        /// <param name="contents">Content to be included.</param>
        /// <param name="files">All files in project.</param>
        /// <param name="nugets">Nugets to be referenced.</param>
        /// <param name="imports">Projects to be referenced.</param>
        /// <returns></returns>
        public static AssemblyBase MakeLibraryProject(string baseNamespace, string name, List<IGenerableFile> includes, List<ContentInfo> contents,
            List<IGenerableFile> files, List<PackageConfigInfo> nugets, List<AssemblyBase> imports)
        {
            AssemblyBase library = new LibraryAssemblyFileGenerator(new List<IGenerableFile>(), new AssemblyInfo
            {
                Name = name,
                ProjectFileRelativePath = name
            });
            library.AssemblyInfo.Contents.AddRange(contents);

            files.Add(library);

            library.AssemblyInfo.AssemblyImports.AddRange(imports);
            library.AssemblyInfo.FilesToCompile.AddRange(includes);
            library.AssemblyInfo.Packages.AddRange(nugets);

            return library;
        }

        /// <summary>
        /// Handles solution registrations.
        /// </summary>
        /// <param name="layerName">Base namespace in project.</param>
        /// <param name="includes">Files to be included.</param>
        /// <param name="contents">Content to be included.</param>
        /// <param name="files">All files in project.</param>
        /// <param name="projectType">Type of project.</param>
        /// <param name="nugets">Nugets to be referenced.</param>
        /// <param name="imports">Projects to be referenced.</param>
        /// <param name="container">Container where to register project files.</param>
        public static void RegisterSolutionLayer(string layerName, ProjectType projectType, List<PackageConfigInfo> nugets, List<IGenerableFile> files, List<IGenerableFile> includes, List<ContentInfo> contents, List<AssemblyBase> imports, WindsorContainer container, List<AzureServiceBusQueue> queues = null, List<AzureEventHub> hubs = null)
        {
            IAssembly instance;
            switch (projectType)
            {
                case ProjectType.FunctionApp:
                    instance = MakeFunctionAppProject(layerName,
                        layerName, includes, contents, files,
                        nugets,
                        imports,
                        queues,
                        hubs);
                    break;

                case ProjectType.Library:
                    instance = MakeLibraryProject(layerName,
                    layerName, includes, contents, files,
                    nugets,
                    imports);
                    break;

                default:
                    throw new NotSupportedException("This type of project is not supported");
            }
            container.Register(
               Component.For<IAssembly>().Instance(instance
                       ).Named(layerName));

        }

        /// <summary>
        /// Resolvse container handlers into IGenerableFiles
        /// </summary>
        /// <param name="handlers">Handlers to be resolved.</param>
        /// <param name="container">Container containing IGenerableFiles</param>
        /// <param name="projectName">Project name.</param>
        /// <returns>List of resolved IGenerableFiles.</returns>
        public static List<IGenerableFile> ResolveHandlers(IHandler[] handlers, WindsorContainer container, string projectName = null)
        {
            List<IGenerableFile> output = new List<IGenerableFile>();
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (path == null)
            {
                return output;
            }

            List<IHandler> additionalHandlers = container.Kernel.GetAssignableHandlers(typeof(IGenerableFile)).ToList();

            foreach (var handler in handlers)
            {
                var type = handler.ComponentModel.Implementation;

                var generatorDependenciesType = Utils.LoadTypes(path).FirstOrDefault(t => t.BaseType != null && t.BaseType.IsGenericType &&
                                                                  t.BaseType.GetGenericTypeDefinition() ==
                                                                  typeof(GeneratorDependency<>) &&
                                                                  !typeof(IServerless).IsAssignableFrom(t) &&
                                                                  t.BaseType.GenericTypeArguments.Contains(
                                                                     type));
                if (generatorDependenciesType != null)
                {
                    var generatorDependencies =
                        ((GeneratorDependency)Activator.CreateInstance(generatorDependenciesType)).GetRegistrations(
                            projectName);
                    foreach (var registration in generatorDependencies)
                    {
                        try
                        {
                            container.Register(registration);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                }


            }
            foreach (var handler in handlers)
            {
                var file = container.Resolve<IGenerableFile>(handler.ComponentModel.Name);
                output.Add(file);
            }
            additionalHandlers = container.Kernel.GetAssignableHandlers(typeof(IGenerableFile)).ToList().Except(additionalHandlers).ToList();
            foreach (var handler in additionalHandlers)
            {
                var file = container.Resolve<IGenerableFile>(handler.ComponentModel.Name);
                output.Add(file);
            }

            return output;
        }
    }
}
