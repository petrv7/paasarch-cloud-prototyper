using CloudPrototyper.Model.Operations;
using System.Collections.Generic;

namespace CloudPrototyper.NET.Framework.v462.Computing.Models
{
    public class SimulateComputation : Operation
    {
        public int MsLength { get; set; }
        public override List<ResourceReference> GetReferencedResources() => new List<ResourceReference>();
        public override List<string> GetReferencedEntities() => new List<string>();
    }
}
