using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Core.v31.Functions.Generators;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Core.v31.Functions.Registrations
{
    public class StartupRegistrations : GeneratorDependency<StartupGenerator>
    {
        public override List<IRegistration> GetRegistrations(string projectName)
          => new List<IRegistration>
          {
               Component.For<ActionBaseGenerator>()
                    .ImplementedBy<ActionBaseGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
               Component.For<OperationInterfaceGenerator>()
                    .ImplementedBy<OperationInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
               Component.For<StorageInterfaceGenerator>()
                    .ImplementedBy<StorageInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
               Component.For<MessageBusInterfaceGenerator>()
                    .ImplementedBy<MessageBusInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
               Component.For<QueryGenerator>()
                    .ImplementedBy<QueryGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))
          };
    }
}
