using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.DataFactories;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Standard.v20.EventHub.Generators;

namespace CloudPrototyper.NET.Standard.v20.EventHub.Registrations
{
    public class AzureEventHubRegistrations : GeneratorDependency<AzureEventHubGenerator>
    {
        public override List<IRegistration> GetRegistrations(string projectName)
            => new List<IRegistration>
        {
               Component.For<MessageBusInterfaceGenerator>()
                    .ImplementedBy<MessageBusInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
                Component.For<DataGeneratorGenerator>()
                    .ImplementedBy<DataGeneratorGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))
        };
    }
}