using CloudPrototyper.Model.Resources.Storage;

namespace CloudPrototyper.NET.Framework.v462.CosmosDb.Model
{
    public class AzureCosmosDbContainer : RelationalDatabase
    {
        /// <summary>
        /// Throughput type, "manual" or "autoscale"
        /// </summary>
        public string ThroughputType { get; set; }
        /// <summary>
        /// Number of RU/s for manual throughput, max RU/s for autoscale
        /// </summary>
        public int RUs { get; set; }
        /// <summary>
        /// Name of the container database
        /// </summary>
        public string DatabaseName { get; set; } = "";
        /// <summary>
        /// Partition key of the container, default value /id as recommended by Azure
        /// </summary>
        public string PartitionKey { get; set; } = "/id";
    }
}
