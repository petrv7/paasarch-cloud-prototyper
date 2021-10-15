using CloudPrototyper.Model.Applications;

namespace CloudPrototyper.NET.Core.v31.Functions.Model
{
    public class AzureCosmosDbTrigger : Trigger
    {
        public string ContainerName { get; set; }
        public bool ProcessOncePerTrigger { get; set; } = false;

    }
}
