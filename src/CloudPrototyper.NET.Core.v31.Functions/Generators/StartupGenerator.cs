using CloudPrototyper.NET.Core.v31.Functions.Templates;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using System;
using System.Collections.Generic;

namespace CloudPrototyper.NET.Core.v31.Functions.Generators
{
    public class StartupGenerator : CodeGeneratorBase
    {
        public ActionBaseGenerator ActionBase { get; set; }
        public StorageInterfaceGenerator StorageInterface { get; set; }
        public MessageBusInterfaceGenerator MessageBusInterface { get; set; }
        public OperationInterfaceGenerator OperationInterface { get; set; }

        public override List<PackageConfigInfo> GetNugetPackages() => new List<PackageConfigInfo>
        {
            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>("", @"")
            },"Castle.Windsor","5.1.1",""),

            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>("", @"")
            },"Castle.Windsor.MsDependencyInjection","3.4.0",""),

            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>("", @"")
            },"Microsoft.Extensions.DependencyInjection","5.0.1","")
        };

        public StartupGenerator(string projectName, StorageInterfaceGenerator storageInterface, MessageBusInterfaceGenerator messageBusInterface, OperationInterfaceGenerator operationInterface, ActionBaseGenerator actionBase, bool canInitialize = true) : base(projectName, "Utils", "Startup", typeof(StartupTemplate), canInitialize)
        {
            StorageInterface = storageInterface;
            MessageBusInterface = messageBusInterface;
            OperationInterface = operationInterface;
            ActionBase = actionBase;
        }
    }
}
