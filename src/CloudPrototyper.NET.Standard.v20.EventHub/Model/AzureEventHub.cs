using CloudPrototyper.Model.Resources.Storage;
using Newtonsoft.Json;

namespace CloudPrototyper.NET.Standard.v20.EventHub.Model
{
    public class AzureEventHub : Queue
    {
        [JsonIgnore]
        public string ConnectionString { get; set; } = "";

        /// <summary>
        /// Partition count of the Event Hub (default value 2, available values 2 - 32)
        /// </summary>
        public int PartitionCount { get; set; } = 2;
        /// <summary>
        /// Name of the Event Hub Namespace
        /// </summary>
        public string WithNamespace { get; set; }
    }
}
