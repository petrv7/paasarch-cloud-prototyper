using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.DataFactories;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities;
using CloudPrototyper.NET.Framework.v462.EventHub.Model;
using CloudPrototyper.NET.Framework.v462.EventHub.Templates;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Prototyper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudPrototyper.NET.Framework.v462.EventHub.Generators
{
    public class AzureEventHubGenerator : Modeled<AzureEventHub>
    {
        public MessageBusInterfaceGenerator MessageBusInterfaceGenerator { get; set; }
        public DataGeneratorGenerator DataGenerator { get; set; }
        public List<EntityGenerator> Entities { get; set; }
        public override List<PackageConfigInfo> GetNugetPackages() => new()
        {
            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "",
                    @"")
            }, "Azure.Messaging.EventHubs", "5.5.0", "")
        };

        public AzureEventHubGenerator(string projectName, MessageBusInterfaceGenerator messageBusInterfaceGenerator, AzureEventHub modelParameters, DataGeneratorGenerator dataGenerator, IList<EntityGenerator> entities) : base(projectName, modelParameters.Name, modelParameters.Name + "Context", typeof(AzureEventHubTemplate), modelParameters, modelParameters.Name)
        {
            DataGenerator = dataGenerator;
            Entities = entities.ToList();
            MessageBusInterfaceGenerator = messageBusInterfaceGenerator;
        }
    }
}
