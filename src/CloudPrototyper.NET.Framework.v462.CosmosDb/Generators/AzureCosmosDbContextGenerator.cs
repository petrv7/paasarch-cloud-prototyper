using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.DataFactories;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities;
using CloudPrototyper.NET.Framework.v462.CosmosDb.Model;
using CloudPrototyper.NET.Framework.v462.CosmosDb.Templates;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Prototyper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudPrototyper.NET.Framework.v462.CosmosDb.Generators
{
    public class AzureCosmosDbContextGenerator : Modeled<AzureCosmosDbContainer>, IEntityStorage
    {
        public List<EntityGenerator> Entities { get; set; }
        public StorageInterfaceGenerator StorageInterface { get; set; }
        public QueryGenerator Query { get; set; }
        public DataGeneratorGenerator DataGenerator { get; set; }

        public override List<PackageConfigInfo> GetNugetPackages() => new List<PackageConfigInfo>
        {
            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "",
                    @"")
            }, "Microsoft.Azure.Cosmos", "3.20.1", "")
        };

        public AzureCosmosDbContextGenerator(string projectName, AzureCosmosDbContainer modelParameters, IList<EntityGenerator> entities,
    StorageInterfaceGenerator storageInterface, QueryGenerator query, DataGeneratorGenerator dataGenerator)
    : base(
        projectName, modelParameters.Name, modelParameters.Name + "Context", typeof(AzureCosmosDbContextTemplate),
        modelParameters, modelParameters.Name)
        {
            Query = query;
            Entities = entities.Where(x => modelParameters.EntitySets.Select(y => y.EntityName).ToList().Contains(x.Name)).ToList();
            StorageInterface = storageInterface;
            DataGenerator = dataGenerator;
        }
    }
}
