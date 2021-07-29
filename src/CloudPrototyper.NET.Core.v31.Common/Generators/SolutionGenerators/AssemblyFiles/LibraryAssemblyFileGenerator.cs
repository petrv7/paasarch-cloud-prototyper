using System.Collections.Generic;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.NET.Core.v31.Common.Templates.SolutionTemplates.AssemblyFiles;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;

namespace CloudPrototyper.NET.Core.v31.Common.Generators.SolutionGenerators.AssemblyFiles
{ 
    /// <summary>
    /// Generator of csproj file for library projects.
    /// </summary>
    public class LibraryAssemblyFileGenerator : AssemblyBase
    {

        public LibraryAssemblyFileGenerator(List<IGenerableFile> files, AssemblyInfo assemblyInfo) : base(assemblyInfo, files, new GenerationInfo(assemblyInfo.Name + ".csproj", assemblyInfo.ProjectFileRelativePath, new LibraryAssemblyTemplate(), true))
        {
            Files = files;
        }
    }
}
