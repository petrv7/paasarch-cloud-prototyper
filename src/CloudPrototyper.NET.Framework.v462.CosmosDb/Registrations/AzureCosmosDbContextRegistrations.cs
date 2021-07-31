using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities;
using CloudPrototyper.NET.Framework.v462.CosmosDb.Generators;
using CloudPrototyper.NET.Interface.Generation;
using System.Collections.Generic;

namespace CloudPrototyper.NET.Framework.v462.CosmosDb.Registrations
{
    public class AzureCosmosDbContextRegistrations : GeneratorDependency<AzureCosmosDbContextGenerator>
    {

        public override List<IRegistration> GetRegistrations(string projectName)
        => new List<IRegistration>
        {
            Component.For<MessageBusInterfaceGenerator>()
                .ImplementedBy<MessageBusInterfaceGenerator>()
                .LifestyleSingleton()
                .DependsOn(Dependency.OnValue("projectName", projectName)),
            Component.For<StorageInterfaceGenerator>()
                .ImplementedBy<StorageInterfaceGenerator>()
                .LifestyleSingleton()
                .DependsOn(Dependency.OnValue("projectName", projectName)),
            Component.For<EntityInterfaceGenerator>()
                .ImplementedBy<EntityInterfaceGenerator>()
                .LifestyleSingleton()
                .DependsOn(Dependency.OnValue("projectName", projectName)),
            Component.For<QueryGenerator>()
                .ImplementedBy<QueryGenerator>()
                .LifestyleSingleton()
                .DependsOn(Dependency.OnValue("projectName", projectName))
        };
    }
}
