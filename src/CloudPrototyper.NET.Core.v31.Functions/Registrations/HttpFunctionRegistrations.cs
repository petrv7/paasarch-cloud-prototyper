using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.v6.Functions.Registrations
{
    public class HttpFunctionRegistrations : GeneratorDependency<ActionGenerator>, IServerless
    {
        public override List<IRegistration> GetRegistrations(string projectName)
           => new List<IRegistration>
           {
               Component.For<OperationInterfaceGenerator>()
                    .ImplementedBy<OperationInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
               Component.For<StorageInterfaceGenerator>()
                    .ImplementedBy<StorageInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
           };
    }
}
