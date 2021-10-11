using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Model.Resources;

namespace CloudPrototyper.NET.Core.v31.Functions.Model
{ 
    public class AzureFunctionApp : HostingEnvironment, IServerless
    {
        /// <summary>
        /// Plan name of the Function App (default consumption, available values consumption, premium or dedicated)
        /// </summary>
        public string PlanName { get; set; } = "";
        /// <summary>
        /// Performance tier of the Function App (available values EP1, EP2, EP3 (for premium plan), for dedicated same as App Service) 
        /// </summary>
        public string PerformanceTier { get; set; }
    }
}
