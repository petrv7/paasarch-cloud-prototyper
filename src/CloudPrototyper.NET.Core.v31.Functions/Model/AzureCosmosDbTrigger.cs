using CloudPrototyper.Model.Applications;

namespace CloudPrototyper.NET.v6.Functions.Model
{
    public class AzureCosmosDbTrigger : Trigger
    {
        /// <summary>
        /// Name of the container
        /// </summary>
        public string ContainerName { get; set; }
        /// <summary>
        /// If true, specified operation is executed only once per function trigger (one trigger can aggregate multiple container changes), if false, specified operation is executed once per container change
        /// </summary>
        public bool ProcessOncePerTrigger { get; set; } = false;

    }
}
