using System.Collections.Generic;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.Model.Operations;

namespace CloudPrototyper.NET.Framework.v462.Computing.Models
{
    /// <summary>
    /// Operation simulation URL call.
    /// </summary>
    public class CallUrlOperation : Operation
    {
        public string ApplicationName { get; set; }
        public string ActionName { get; set; }
        public string Url { get; set; } = "";
        public override List<ResourceReference> GetReferencedResources()
        {
            return string.IsNullOrEmpty(ApplicationName) ? new List<ResourceReference>() : new List<ResourceReference>() { new ResourceReference(typeof(RestApiApplication), ApplicationName) };
        }

        public override List<string> GetReferencedEntities() => new List<string>();
    }
}
