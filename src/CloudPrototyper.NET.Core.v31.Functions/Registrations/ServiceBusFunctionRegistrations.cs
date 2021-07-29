using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.NET.Core.v31.Functions.Generators.Functions;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Core.v31.Functions.Registrations
{
    /// <summary>
    /// HandlerGenerator registrations.
    /// </summary>
    public class ServiceBusFunctionRegistrations : GeneratorDependency<ServiceBusFunctionGenerator>
    {
        public override List<IRegistration> GetRegistrations(string projectName)
            => new List<IRegistration>
            {
               Component.For<AzureServiceBusQueue>()
                    .ImplementedBy<AzureServiceBusQueue>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))
            };
    }
}
