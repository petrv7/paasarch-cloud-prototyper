using CloudPrototyper.Model.Resources;

namespace CloudPrototyper.NET.Standard.v20.EventHub.Model
{
    public class AzureEventHubNamespace : Resource
    {
        /// <summary>
        /// Pricing tier (available values basic or standard)
        /// </summary>
        public string PricingTier { get; set; }
        /// <summary>
        /// Number of throughput units (available values 1 - 40)
        /// </summary>
        public int ThroughputUnits { get; set; }
        /// <summary>
        /// Enable auto-inflate of throughput units (available only for standard pricing tier)
        /// </summary>
        public bool WithAutoScale { get; set; }
        /// <summary>
        /// Maximum auto-inflate of throughput units (available values 1 - 40)
        /// </summary>
        public int MaxThroughputUnits { get; set; }
    }
}
