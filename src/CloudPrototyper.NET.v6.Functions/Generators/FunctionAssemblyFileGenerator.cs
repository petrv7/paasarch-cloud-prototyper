using System.Collections.Generic;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.NET.Core.v31.Functions.Templates;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;

namespace CloudPrototyper.NET.v6.Functions.Generators
{
    public class FunctionAssemblyFileGenerator : AssemblyBase
    {
        public FunctionAssemblyFileGenerator(List<IGenerableFile> files, AssemblyInfo assemblyInfo) : base(assemblyInfo, files, new GenerationInfo(assemblyInfo.Name + ".csproj", assemblyInfo.ProjectFileRelativePath, new FunctionAssemblyFileTemplate(), true))
        {
        }
    }
}
