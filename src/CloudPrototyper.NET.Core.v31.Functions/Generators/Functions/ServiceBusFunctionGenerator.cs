using System;
using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.NET.Core.v31.Functions.Templates.Functions;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Core.v31.Functions.Generators.Functions
{
    /// <summary>
    /// Service Bus Function
    /// </summary>
   public class ServiceBusFunctionGenerator : Modeled<IList<TriggeredAction>>
    {
        public AzureServiceBusQueue AzureServiceBusQueue { get; set; }
        public OperationInterfaceGenerator OperationInterface { get; set; }
        public IList<ActionGenerator> Actions { get; set; }
        public override List<PackageConfigInfo> GetNugetPackages() => new()
        {
            new PackageConfigInfo(new(), "Microsoft.Azure.WebJobs.Extensions.ServiceBus", "4.1.0", "")
        };

        public ServiceBusFunctionGenerator(string projectName, AzureServiceBusQueue azureServiceBusQueue,
            IList<TriggeredAction> modelParameters, IList<ActionGenerator> actions, OperationInterfaceGenerator operationInterface)
            : base(
                projectName, "Functions", azureServiceBusQueue.Name + "Function", typeof(ServiceBusFunctionTemplate),
                modelParameters, azureServiceBusQueue.Name + "Function")
        {
            Actions = actions.Where(x => modelParameters.Select(y => y.Name).Contains(x.Key)).ToList();
            OperationInterface = operationInterface;
            AzureServiceBusQueue = azureServiceBusQueue;
        }
    }
}
