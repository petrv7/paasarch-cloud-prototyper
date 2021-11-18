using System;
using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.NET.Core.v31.Functions.Templates.Functions;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Prototyper;
using CloudPrototyper.NET.Standard.v20.EventHub.Model;

namespace CloudPrototyper.NET.v6.Functions.Generators.Functions
{
    /// <summary>
    /// Service Bus Function
    /// </summary>
    public class EventHubFunctionGenerator : Modeled<IList<TriggeredAction>>
    {
        public AzureEventHub AzureEventHub { get; set; }
        public OperationInterfaceGenerator OperationInterface { get; set; }
        public IList<ActionGenerator> Actions { get { return _actions; }  }
        private readonly IList<ActionGenerator> _actions;
        public override List<PackageConfigInfo> GetNugetPackages() => new()
        {
            new PackageConfigInfo(new(), "Microsoft.Azure.WebJobs.Extensions.EventHubs", "4.3.0", "")
        };

        public EventHubFunctionGenerator(string projectName, AzureEventHub azureEventHub,
            IList<TriggeredAction> modelParameters, IList<ActionGenerator> actions, OperationInterfaceGenerator operationInterface)
            : base(
                projectName, "Functions", azureEventHub.Name + "Function", typeof(EventHubFunctionTemplate),
                modelParameters, azureEventHub.Name + "Function")
        {
            _actions = actions.Where(x => modelParameters.Select(y => y.Name).Contains(x.Key)).ToList();
            OperationInterface = operationInterface;
            AzureEventHub = azureEventHub;
        }
    }
}
