using CloudPrototyper.Model.Resources.Storage;
using Newtonsoft.Json;

namespace CloudPrototyper.NET.Standard.v20.EventHub.Model
{
    public class AzureEventHub : Queue
    {
        [JsonIgnore]
        public string ConnectionString { get; set; } = "";
        public int PartitionCount { get; set; }
        public string WithNamespace { get; set; }
    }
}
