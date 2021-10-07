using System;
using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.NET.Framework.v462.Common.Factories;
using CloudPrototyper.NET.Framework.v462.Common.Templates.BusinessLayerTemplates.Services;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators.Services
{
    /// <summary>
    /// Service Queue Handeler.
    /// </summary>
    public class HandlerGenerator : Modeled<IList<TriggeredAction>>
    {
        public AzureServiceBusQueue AzureServiceBusQueue { get; set; }
        public MessageBusHandlerInterfaceGenerator MessageBusHandlerInterface { get; set; }
        public OperationInterfaceGenerator OperationInterface { get; set; }
        public IList<ActionGenerator> Actions => _actions;
        private readonly IList<ActionGenerator> _actions;
        public override List<PackageConfigInfo> GetNugetPackages() => NugetFactory.MakeAzureServiceBusNuget();
        public HandlerGenerator(string projectName, AzureServiceBusQueue azureServiceBusQueue,
            MessageBusHandlerInterfaceGenerator messageBusHandlerInterface, IList<TriggeredAction> modelParameters, IList<ActionGenerator> actions , OperationInterfaceGenerator operationInterface)
            : base(
                projectName, "Services", azureServiceBusQueue.Name + "Handler", typeof (HandlerTemplate),
                modelParameters, azureServiceBusQueue.Name + "Handler")
        {
            _actions = actions.Where(x => modelParameters.Select(y => y.Name).Contains(x.Key)).ToList();
            OperationInterface = operationInterface;
            AzureServiceBusQueue = azureServiceBusQueue;
            MessageBusHandlerInterface = messageBusHandlerInterface;
        }
    }
}
