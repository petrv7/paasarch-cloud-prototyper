using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.NET.Core.v31.Functions.Templates.Functions;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Core.v31.Functions.Generators.Functions
{
    public class HttpFunctionGenerator : CodeGeneratorBase
    {
        public ActionGenerator Action { get; set; }
        public OperationInterfaceGenerator OperationInterface { get; set; }
        public StorageInterfaceGenerator StorageInterface { get; set; }
        public HttpFunctionGenerator(string projectName, string actionName, IList<ActionGenerator> actions, StorageInterfaceGenerator storageInterfaceGenerator, OperationInterfaceGenerator operationInterface) : base(projectName, "Functions", actions.Single(x => x.Key == actionName).Key + "Action", typeof(HttpFunctionTemplate))
        {
            OperationInterface = operationInterface;
            Action = actions.Single(x => x.Key == actionName);
            StorageInterface = storageInterfaceGenerator;
        }
    }
}
