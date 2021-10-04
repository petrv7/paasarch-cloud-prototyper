using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Core.v31.Functions.Generators.Functions;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Standard.v20.EventHub.Model;

namespace CloudPrototyper.NET.Core.v31.Functions.Registrations
{
    /// <summary>
    /// EventHubFunctionGenerator registrations.
    /// </summary>
    public class EventHubFunctionRegistrations : GeneratorDependency<EventHubFunctionGenerator>
    {
        public override List<IRegistration> GetRegistrations(string projectName)
            => new List<IRegistration>
            {
               Component.For<AzureEventHub>()
                    .ImplementedBy<AzureEventHub>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))
            };
    }
}
