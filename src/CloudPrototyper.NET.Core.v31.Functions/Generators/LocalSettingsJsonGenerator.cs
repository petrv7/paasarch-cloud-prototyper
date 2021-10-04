using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;
using System.Collections.Generic;
using CloudPrototyper.NET.Standard.v20.EventHub.Model;

namespace CloudPrototyper.NET.Core.v31.Functions.Generators
{
    public class LocalSettingsJsonGenerator : GeneratorBase
    {
        public List<AzureServiceBusQueue> ServiceBusQueues { get; set; } = new(); 
        public List<AzureEventHub> EventHubs { get; set; } = new();
        public LocalSettingsJsonGenerator(GenerationInfo generationInfo, List<AzureServiceBusQueue> serviceBusQueues = null, List<AzureEventHub> eventHubs = null) : base(generationInfo)
        {
            if (serviceBusQueues != null)
            {
                ServiceBusQueues = serviceBusQueues;
            }
            
            if (eventHubs != null)
            {
                EventHubs = eventHubs;
            }
        }
    }
}
