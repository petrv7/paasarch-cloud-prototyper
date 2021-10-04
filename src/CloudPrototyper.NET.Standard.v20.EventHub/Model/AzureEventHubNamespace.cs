using CloudPrototyper.Model.Resources;

namespace CloudPrototyper.NET.Standard.v20.EventHub.Model
{
    public class AzureEventHubNamespace : Resource
    {
        public string PricingTier { get; set; }
        public int ThroughputUnits { get; set; }
        public bool WithAutoScale { get; set; }
        public int MaxThroughputUnits { get; set; }
    }
}
