using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Core.v31.Functions.Generators.Functions;
using CloudPrototyper.NET.Framework.v462.EventHub.Model;
using CloudPrototyper.NET.Interface.Generation;

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
