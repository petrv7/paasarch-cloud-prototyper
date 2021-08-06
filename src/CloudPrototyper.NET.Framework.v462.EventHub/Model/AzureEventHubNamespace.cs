using CloudPrototyper.Model.Resources;

namespace CloudPrototyper.NET.Framework.v462.EventHub.Model
{
    public class AzureEventHubNamespace : Resource
    {
        public string PricingTier { get; set; }
        public int ThroughputUnits { get; set; }
    }
}
