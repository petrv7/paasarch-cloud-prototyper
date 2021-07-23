using System;
using System.Collections.Generic;
using CloudPrototyper.NET.Interface.Generation.Informations;

namespace CloudPrototyper.NET.Framework.v462.Common.Factories
{
    /// <summary>
    /// Factory delivering instances of NuGet pacakges.
    /// </summary>
    public static class NugetFactory
    {
        /// <summary>
        /// Service bus nuget.
        /// </summary>
        /// <returns>Package info.</returns>
        public static List<PackageConfigInfo> MakeAzureServiceBusNuget() => new List<PackageConfigInfo>
        {
            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "Microsoft.Azure.ServiceBus, Version=5.1.3.0, Culture=neutral, PublicKeyToken=7e34167dcc6d6d8c, processorArchitecture=MSIL",
                    @"..\packages\Microsoft.Azure.ServiceBus.5.1.3\lib\netstandard2.0\Microsoft.Azure.ServiceBus.dll")
            },"Microsoft.Azure.ServiceBus","5.1.3","net462")
        };

        /// <summary>
        /// Entity Framework.
        /// </summary>
        /// <returns>Package info.</returns>
        public static List<PackageConfigInfo> MakeEntityFrameworkNuget() => new List<PackageConfigInfo>
        {

            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processorArchitecture=MSIL",
                    @"..\packages\EntityFramework.6.4.4\lib\net45\EntityFramework.dll"),
                new Tuple<string, string>(
                    "EntityFramework.SqlServer, Version=6.0.0.0, Culture = neutral, PublicKeyToken = b77a5c561934e089, processorArchitecture = MSIL",
                    @"..\packages\EntityFramework.6.4.4\lib\net45\EntityFramework.SqlServer.dll")
            }, "EntityFramework", "6.4.4", "net462")

        };

        /// <summary>
        /// Azure Table Storage.
        /// </summary>
        /// <returns>Package info.</returns>
        public static List<PackageConfigInfo> MakeAzureTableStorage() => new List<PackageConfigInfo>
        {

            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "Microsoft.Azure.KeyVault.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                    @"..\packages\Microsoft.Azure.KeyVault.Core.1.0.0\lib\net40\Microsoft.Azure.KeyVault.Core.dll")
            }, "Microsoft.Azure.KeyVault.Core", "1.0.0", "net462"),

            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "Microsoft.Data.Edm, Version=5.6.4.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                    @"..\packages\Microsoft.Data.Edm.5.6.4\lib\net40\Microsoft.Data.Edm.dll")
            }, "Microsoft.Data.Edm", "5.6.4", "net462"),


            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "Microsoft.Data.OData, Version=5.6.4.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                    @"..\packages\Microsoft.Data.OData.5.6.4\lib\net40\Microsoft.Data.OData.dll")
            }, "Microsoft.Data.OData", "5.6.4", "net462"),


            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "Microsoft.Data.Services.Client, Version=5.6.4.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"
                    ,
                    @"..\packages\Microsoft.Data.Services.Client.5.6.4\lib\net40\Microsoft.Data.Services.Client.dll")
            }, "Microsoft.Data.Services.Client", "5.6.4", "net462"),



            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Newtonsoft.Json, Version=10.0.0.0, Culture=neutral, " +
                                          "PublicKeyToken=30ad4fe6b2a6aeed, processorArchitecture=MSIL",
                    @"..\packages\Newtonsoft.Json.10.0.3\lib\net45\Newtonsoft.Json.dll")
            }, "Newtonsoft.Json", "10.0.3", "net462"),


            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "System.Spatial, Version=5.6.4.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                    @"..\packages\System.Spatial.5.6.4\lib\net40\System.Spatial.dll")
            }, "System.Spatial", "5.6.4", "net462"),



            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "Microsoft.WindowsAzure.Storage, Version=7.2.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                    @"..\packages\WindowsAzure.Storage.7.2.1\lib\net40\Microsoft.WindowsAzure.Storage.dll")
            }, "WindowsAzure.Storage", "7.2.1", "net462"),

        };


        /// <summary>
        /// Castle Windsor.
        /// </summary>
        /// <returns>Package info.</returns>
        public static List<PackageConfigInfo> MakeCastleWidsorNuget() => new List<PackageConfigInfo>
        {

            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "Castle.Windsor, Version=3.4.0.0, Culture=neutral, PublicKeyToken=407dd0808d44fbdc, processorArchitecture=MSIL",
                    @"..\packages\Castle.Windsor.3.4.0\lib\net45\Castle.Windsor.dll")
            }, "Castle.Windsor", "3.4.0", "net462"),


            new PackageConfigInfo(

                new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "Castle.Core, Version=3.3.0.0, Culture=neutral, PublicKeyToken=407dd0808d44fbdc, processorArchitecture=MSIL",
                        @"..\packages\Castle.Core.3.3.0\lib\net45\Castle.Core.dll")
                }, "Castle.Core", "3.3.0", "net462")

        };

        /// <summary>
        /// Microsoft Azure ServiceBus.
        /// </summary>
        /// <returns>Package info.</returns>
        public static List<PackageConfigInfo> MakeMicrosoftAzureServiceBusNuget() => new List<PackageConfigInfo>
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
    }
}

