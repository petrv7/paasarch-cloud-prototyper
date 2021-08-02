using System;
using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Framework.v462.EventHub.Model;
using CloudPrototyper.NET.Framework.v462.EventHub.Templates;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Framework.v462.EventHub.Generators
{
    public class AzureEventHubHandlerGenerator : Modeled<IList<TriggeredAction>>
    {
        public AzureEventHub AzureEventHub { get; set; }
        public MessageBusHandlerInterfaceGenerator MessageBusHandlerInterface { get; set; }
        public OperationInterfaceGenerator OperationInterface { get; set; }
        public IList<ActionGenerator> Actions { get; set; }
        public override List<PackageConfigInfo> GetNugetPackages() => new()
        {
            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "",
                    @"")
            }, "Azure.Messaging.EventHubs", "5.5.0", "")
        };

        public AzureEventHubHandlerGenerator(string projectName, AzureEventHub azureEventHub,
            MessageBusHandlerInterfaceGenerator messageBusHandlerInterface, IList<TriggeredAction> modelParameters, IList<ActionGenerator> actions, OperationInterfaceGenerator operationInterface)
            : base(
                projectName, "Services", azureEventHub.Name + "Handler", typeof(AzureEventHubHandlerTemplate),
                modelParameters, azureEventHub.Name + "Handler")
        {
            Actions = actions.Where(x => modelParameters.Select(y => y.Name).Contains(x.Key)).ToList();
            OperationInterface = operationInterface;
            AzureEventHub = azureEventHub;
            MessageBusHandlerInterface = messageBusHandlerInterface;
        }
    }
}