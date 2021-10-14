using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.NET.Core.v31.Functions.Model;
using CloudPrototyper.NET.Core.v31.Functions.Templates.Functions;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Standard.v20.CosmosDb.Model;

namespace CloudPrototyper.NET.Core.v31.Functions.Generators.Functions
{
    public class CosmosDbFunctionGenerator : CodeGeneratorBase
    {
        public ActionGenerator Action { get; set; }
        public AzureCosmosDbContainer Container { get; set; }
        public OperationInterfaceGenerator OperationInterface { get; set; }
        public AzureCosmosDbTrigger Trigger { get; set; }
        public override List<PackageConfigInfo> GetNugetPackages() => new()
        {
            new PackageConfigInfo(new(), "Microsoft.Azure.WebJobs.Extensions.CosmosDB", "3.0.5", "")
        };
        public CosmosDbFunctionGenerator(AzureCosmosDbTrigger trigger, string projectName, AzureCosmosDbContainer container, string actionName, IList<ActionGenerator> actions, OperationInterfaceGenerator operationInterface) : base(projectName, "Functions", actions.Single(x => x.Key == actionName).Key + "Action", typeof(CosmosDbFunctionTemplate))
        {
            OperationInterface = operationInterface;
            Action = actions.Single(x => x.Key == actionName);
            Container = container;
            Trigger = trigger;
        }
    }
}