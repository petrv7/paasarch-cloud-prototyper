﻿using CloudPrototyper.NET.Core.v31.Functions.Templates;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Core.v31.Functions.Generators
{
    public class StartupGenerator : CodeGeneratorBase
    {
        public ActionBaseGenerator ActionBase { get; set; }
        public StorageInterfaceGenerator StorageInterface { get; set; }
        public MessageBusInterfaceGenerator MessageBusInterface { get; set; }
        public MessageBusHandlerInterfaceGenerator MessageBusHandlerInterface { get; set; }
        public OperationInterfaceGenerator OperationInterface { get; set; }        

        public StartupGenerator(string projectName, StorageInterfaceGenerator storageInterface, MessageBusInterfaceGenerator messageBusInterface, MessageBusHandlerInterfaceGenerator messageBusHandlerInterface, OperationInterfaceGenerator operationInterface, ActionBaseGenerator actionBase, bool canInitialize = true) : base(projectName, "Utils", "Startup", typeof(StartupTemplate), canInitialize)
        {
            StorageInterface = storageInterface;
            MessageBusInterface = messageBusInterface;
            MessageBusHandlerInterface = messageBusHandlerInterface;
            OperationInterface = operationInterface;
            ActionBase = actionBase;
        }
    }
}