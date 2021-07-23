using System;
using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.NET.Framework.v462.Common.Templates.BusinessLayerTemplates.Services;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators.Services
{
    /// <summary>
    /// Service Queue Handeler.
    /// </summary>
    public class HandlerGenerator : Modeled<IList<TriggeredAction>>
    {
        public AzureServiceBusQueue AzureServiceBusQueue { get; set; }
        public MessageBusHandlerInterfaceGenerator MessageBusHandlerInterface { get; set; }
        public OperationInterfaceGenerator OperationInterface { get; set; }
        public IList<ActionGenerator> Actions { get; set; }
        public override List<PackageConfigInfo> GetNugetPackages() => new List<PackageConfigInfo>
        {
            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "Microsoft.Azure.Amqp, Version=2.4.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                        @"..\packages\Microsoft.Azure.Amqp.2.4.11\lib\net45\Microsoft.Azure.Amqp.dll")
                },"Microsoft.Azure.Amqp","2.4.11","net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "Microsoft.Azure.ServiceBus, Version=5.1.3.0, Culture=neutral, PublicKeyToken=7e34167dcc6d6d8c, processorArchitecture=MSIL",
                        @"..\packages\Microsoft.Azure.ServiceBus.5.1.3\lib\netstandard2.0\Microsoft.Azure.ServiceBus.dll")
                },"Microsoft.Azure.ServiceBus","5.1.3","net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "Microsoft.Azure.Services.AppAuthentication, Version=1.0.3.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                        @"..\packages\Microsoft.Azure.Services.AppAuthentication.1.0.3\lib\net452\Microsoft.Azure.Services.AppAuthentication.dll")
                },"Microsoft.Azure.Services.AppAuthentication","1.0.3","net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "Microsoft.IdentityModel.Clients.ActiveDirectory, Version=3.14.2.11, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                        @"..\packages\Microsoft.IdentityModel.Clients.ActiveDirectory.3.14.2\lib\net45\Microsoft.IdentityModel.Clients.ActiveDirectory.dll")
                },"Microsoft.IdentityModel.Clients.ActiveDirectory","3.14.2","net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "Microsoft.IdentityModel.Clients.ActiveDirectory.Platform, Version=3.14.2.11, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                        @"..\packages\Microsoft.IdentityModel.Clients.ActiveDirectory.3.14.2\lib\net45\Microsoft.IdentityModel.Clients.ActiveDirectory.Platform.dll")
                },"Microsoft.IdentityModel.Clients.ActiveDirectory.Platform","3.14.2","net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "Microsoft.IdentityModel.JsonWebTokens, Version=5.4.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                        @"..\packages\Microsoft.IdentityModel.JsonWebTokens.5.4.0\lib\net461\Microsoft.IdentityModel.JsonWebTokens.dll")
                },"Microsoft.IdentityModel.JsonWebTokens","5.4.0","net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "Microsoft.IdentityModel.Logging, Version=5.4.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                        @"..\packages\Microsoft.IdentityModel.Logging.5.4.0\lib\net461\Microsoft.IdentityModel.Logging.dll")
                },"Microsoft.IdentityModel.Logging","5.4.0","net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "Microsoft.IdentityModel.Tokens, Version=5.4.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                        @"..\packages\Microsoft.IdentityModel.Tokens.5.4.0\lib\net461\Microsoft.IdentityModel.Tokens.dll")
                },"Microsoft.IdentityModel.Tokens","5.4.0","net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "System.Diagnostics.DiagnosticSource, Version=4.0.3.1, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51, processorArchitecture=MSIL",
                        @"..\packages\System.Diagnostics.DiagnosticSource.4.5.1\lib\net46\System.Diagnostics.DiagnosticSource.dll")
                },"System.Diagnostics.DiagnosticSource","4.5.1","net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "System.IdentityModel.Tokens.Jwt, Version=5.4.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                        @"..\packages\System.IdentityModel.Tokens.Jwt.5.4.0\lib\net461\System.IdentityModel.Tokens.Jwt.dll")
                },"System.IdentityModel.Tokens.Jwt","5.4.0","net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "System.Runtime.Serialization.Primitives, Version=4.1.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL",
                        @"..\packages\System.Runtime.Serialization.Primitives.4.1.1\lib\net46\System.Runtime.Serialization.Primitives.dll")
                },"System.Runtime.Serialization.Primitives","4.1.1","net462"),


            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "System.Net.WebSockets, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL",
                        @"..\packages\System.Net.WebSockets.4.0.0\lib\net46\System.Net.WebSockets.dll")
                },"System.Net.WebSockets","4.0.0","net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "System.Net.WebSockets.Client, Version=4.0.0.1, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL",
                        @"..\packages\System.Net.WebSockets.Client.4.0.2\lib\net46\System.Net.WebSockets.Client.dll")
                },"System.Net.WebSockets.Client","4.0.2","net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "System.Runtime.Serialization.Primitives, Version=4.1.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL",
                        @"..\packages\System.Runtime.Serialization.Primitives.4.1.1\lib\net46\System.Runtime.Serialization.Primitives.dll")
                },"System.Runtime.Serialization.Primitives","4.1.1","net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "System.Security.Cryptography.Algorithms, Version=4.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL",
                        @"..\packages\System.Security.Cryptography.Algorithms.4.2.0\lib\net461\System.Security.Cryptography.Algorithms.dll")
                },"System.Security.Cryptography.Algorithms","4.2.0","net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "System.Security.Cryptography.Encoding, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL",
                        @"..\packages\System.Security.Cryptography.Encoding.4.0.0\lib\net46\System.Security.Cryptography.Encoding.dll")
                },"System.Security.Cryptography.Encoding","4.0.0","net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "System.Security.Cryptography.Primitives, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL",
                        @"..\packages\System.Security.Cryptography.Primitives.4.0.0\lib\net46\System.Security.Cryptography.Primitives.dll")
                },"System.Security.Cryptography.Primitives","4.0.0","net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "System.Security.Cryptography.X509Certificates, Version=4.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL",
                        @"..\packages\System.Security.Cryptography.X509Certificates.4.1.0\lib\net461\System.Security.Cryptography.X509Certificates.dll")
                },"System.Security.Cryptography.X509Certificates","4.1.0","net462")

        };
        public HandlerGenerator(string projectName, AzureServiceBusQueue azureServiceBusQueue,
            MessageBusHandlerInterfaceGenerator messageBusHandlerInterface, IList<TriggeredAction> modelParameters, IList<ActionGenerator> actions , OperationInterfaceGenerator operationInterface)
            : base(
                projectName, "Services", azureServiceBusQueue.Name + "Handler", typeof (HandlerTemplate),
                modelParameters, azureServiceBusQueue.Name + "Handler")
        {
            Actions = actions.Where(x => modelParameters.Select(y => y.Name).Contains(x.Key)).ToList();
            OperationInterface = operationInterface;
            AzureServiceBusQueue = azureServiceBusQueue;
            MessageBusHandlerInterface = messageBusHandlerInterface;
        }
    }
}
