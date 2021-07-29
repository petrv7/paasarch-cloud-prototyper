using System;
using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.NET.Framework.v462.Common.Factories;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.DataFactories;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities;
using CloudPrototyper.NET.Framework.v462.TblStorage.Model;
using CloudPrototyper.NET.Framework.v462.TblStorage.Templates;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Prototyper;
using QueryGenerator = CloudPrototyper.NET.Interface.Generation.QueryGenerator;

namespace CloudPrototyper.NET.Framework.v462.TblStorage.Generators
{
    /// <summary>
    /// Generator of Azure table storage context.
    /// </summary>
    public class AzureTableStorageContextGenerator : Modeled<AzureTableStorage>, IEntityStorage
    {
        public List<EntityGenerator> Entities { get; set; }
        public StorageInterfaceGenerator StorageInterface { get; set; }
        public QueryGenerator Query { get; set; }
        public DataGeneratorGenerator DataGenerator { get; set; }
        public override List<PackageConfigInfo> GetNugetPackages() => NugetFactory.MakeAzureTableStorage();

        public AzureTableStorageContextGenerator(string projectName, AzureTableStorage modelParameters, IList<EntityGenerator> entities,
            StorageInterfaceGenerator storageInterface, QueryGenerator query, DataGeneratorGenerator dataGenerator)
            : base(
                projectName, modelParameters.Name, modelParameters.Name+"Context", typeof(AzureTableStorageContextTemplate),
                modelParameters, modelParameters.Name)
        {
            Query = query;
            Entities = entities.Where(x => modelParameters.EntitySets.Select(y => y.EntityName).ToList().Contains(x.Name)).ToList();
            StorageInterface = storageInterface;
            DataGenerator = dataGenerator;
        }
    }
}
