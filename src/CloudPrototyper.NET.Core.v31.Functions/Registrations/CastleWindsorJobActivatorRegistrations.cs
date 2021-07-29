using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Core.v31.Functions.Generators;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Core.v31.Functions.Registrations
{
    public class CastleWindsorJobActivatorRegistrations : GeneratorDependency<CastleWindsorJobActivatorGenerator>
    {
        public override List<IRegistration> GetRegistrations(string projectName)
          => new List<IRegistration>
          {
               Component.For<StartupGenerator>()
                    .ImplementedBy<StartupGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))
          };
    }
}
