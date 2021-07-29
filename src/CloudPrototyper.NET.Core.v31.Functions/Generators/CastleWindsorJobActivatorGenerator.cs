﻿using CloudPrototyper.NET.Core.v31.Functions.Templates;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using System;
using System.Collections.Generic;

namespace CloudPrototyper.NET.Core.v31.Functions.Generators
{
    public class CastleWindsorJobActivatorGenerator : CodeGeneratorBase
    {
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
        public CastleWindsorJobActivatorGenerator(string projectName, bool canInitialize = true) : base(projectName, "Utils", "CastleWindsorJobActivator", typeof(CastleWindsorJobActivatorTemplate), canInitialize)
        {
        }
    }
}
