using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Model.Resources;

namespace CloudPrototyper.Azure.Resources
{ 
    public class AzureFunctionApp : HostingEnvironment, IServerless
    {
        public string PlanName { get; set; } = "";

        public string PerformanceTier { get; set; }

        public string WithApplication { get; set; }
    }
}
