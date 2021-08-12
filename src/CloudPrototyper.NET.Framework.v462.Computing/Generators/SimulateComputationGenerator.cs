using CloudPrototyper.NET.Framework.v462.Computing.Models;
using CloudPrototyper.NET.Framework.v462.Computing.Templates;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Framework.v462.Computing.Generators
{
    public class SimulateComputationGenerator : Modeled<SimulateComputation>, IOperation
    {
        public OperationInterfaceGenerator OperationInterface { get; set; }

        public SimulateComputationGenerator(string projectName, OperationInterfaceGenerator operationInterface, SimulateComputation modelParameters, bool canInitialize = true) : base(projectName, "Operations", modelParameters.Name, typeof(SimulateComputationTemplate), modelParameters, modelParameters.Name, canInitialize)
        {
            OperationInterface = operationInterface;
        }
    }
}
