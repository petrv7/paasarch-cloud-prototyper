using CloudPrototyper.NET.Core.v31.Functions.Templates;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Core.v31.Functions.Generators
{
    public class CastleWindsorJobActivatorGenerator : CodeGeneratorBase
    {
        public CastleWindsorJobActivatorGenerator(string projectName, bool canInitialize = true) : base(projectName, "Utils", "CastleWindsorJobActivator", typeof(CastleWindsorJobActivatorTemplate), canInitialize)
        {
        }
    }
}
