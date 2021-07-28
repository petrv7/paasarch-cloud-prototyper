using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;
using System.Collections.Generic;

namespace CloudPrototyper.NET.Core.v31.Functions.Generators
{
    public class LocalSettingsJsonGenerator : GeneratorBase
    {
        public List<AzureServiceBusQueue> Queues { get; set; } = new();
        public LocalSettingsJsonGenerator(GenerationInfo generationInfo, List<AzureServiceBusQueue> queues = null) : base(generationInfo)
        {
            if (queues != null)
            {
                Queues = queues;
            }
            
        }
    }
}
