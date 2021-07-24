using CloudPrototyper.Model.Resources.Storage;
using Newtonsoft.Json;

namespace CloudPrototyper.NET.Framework.v462.CosmosDb.Model
{
    public class AzureCosmosDbContainer : KeyValueDatabase
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
        [JsonIgnore]
        public string ConnectionString { get; set; } = "";
    }
}
