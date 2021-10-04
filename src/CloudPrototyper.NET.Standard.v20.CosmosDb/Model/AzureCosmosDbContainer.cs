using CloudPrototyper.Model.Resources.Storage;
using Newtonsoft.Json;

namespace CloudPrototyper.NET.Standard.v20.CosmosDb.Model
{
    public class AzureCosmosDbContainer : RelationalDatabase
    {
        /// <summary>
        /// Use serverless account
        /// </summary>
        public bool IsServerless { get; set; } = false;
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
        [JsonIgnore]
        public string DatabaseName { get; set; } = "";
        /// <summary>
        /// Partition key of the container, default value /id as recommended by Azure
        /// </summary>
        public string PartitionKey { get; set; } = "/id";
        /// <summary>
        /// Connection mode of Azure Cosmos DB SQL SDK, see https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-sdk-connection-modes
        /// </summary>
        public bool UseGateway = false;
    }
}
