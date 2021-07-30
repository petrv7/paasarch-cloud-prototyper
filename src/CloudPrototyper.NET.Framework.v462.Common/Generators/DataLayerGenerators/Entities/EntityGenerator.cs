using System;
using System.Collections.Generic;
using CloudPrototyper.Model.Entities;
using CloudPrototyper.NET.Framework.v462.Common.Factories;
using CloudPrototyper.NET.Framework.v462.Common.Templates.DataLayerTemplates.Entities;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities
{
    /// <summary>
    /// Generator for entity classes.
    /// </summary>
    public class EntityGenerator : Modeled<Entity>
    {
        public EntityInterfaceGenerator EntityInterface { get; set; }
        public override List<PackageConfigInfo> GetNugetPackages() 
        {
            var nugets = new List<PackageConfigInfo>();

            nugets.AddRange(NugetFactory.MakeEntityFrameworkNuget());
            nugets.AddRange(NugetFactory.MakeAzureTableStorage());
            
            nugets.Add(new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Newtonsoft.Json, Version=10.0.0.0, Culture=neutral, " +
                                          "PublicKeyToken=30ad4fe6b2a6aeed, processorArchitecture=MSIL",
                    @"..\packages\Newtonsoft.Json.10.0.3\lib\net45\Newtonsoft.Json.dll")
            }, "Newtonsoft.Json", "11.0.2", "net462"));
            
            nugets.Add(new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "",
                    @"")
            }, "Microsoft.Azure.Cosmos", "3.20.1", ""));

            return nugets;
        }

        public EntityGenerator(string projectName, EntityInterfaceGenerator entityInterfaceGenerator, Entity modelParameters) : base(projectName, "Entities", modelParameters.Name, typeof(EntityTemplate), modelParameters, modelParameters.Name)
        {
            EntityInterface = entityInterfaceGenerator;
        }
    }
}
