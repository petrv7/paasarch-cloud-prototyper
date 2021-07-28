using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using CloudPrototyper.Azure.Resources;
using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.Interface.Deployment;
using CloudPrototyper.Model.Applications;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.Sql.Fluent;
using Resource = CloudPrototyper.Model.Resources.Resource;
using CloudPrototyper.NET.Framework.v462.TblStorage.Model;
using FluentFTP;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.Management.Sql.Fluent.Models;
using Microsoft.Azure.Management.Storage.Fluent;

namespace CloudPrototyper.Deployment.Azure
{
    /// <summary>
    /// The implementation of DeploymentManager for Azure Cloud. 
    /// Before usage it is needed to have Service App - Owner with config keys:
    /// "ClientId"
    /// "TennantId" 
    /// "ClientSecret" 
    /// "SubscriptionId"
    /// 
    /// The subscription need several registrations via PowerShell:
    /// Login-AzureRmAccount
    /// Register-AzureRmResourceProvider 
    /// a) -ProviderNamespace Microsoft.DataFactory
    /// b) -ProviderNamespace Microsoft.Cache
    /// c) -ProviderNamespace Microsoft.Storage
    /// d) -ProviderNamespace Microsoft.ServiceBus
    /// </summary>
    public class AzureDeploymentManager : DeploymentManager
    {
        private IAzure _azure;
        private IResourceGroup _resourceGroup;
        private ISqlServer _sqlServer;
        private IStorageAccount _storageAccount;
        private readonly Dictionary<string, string> _queueConnStr = new();
        private readonly Dictionary<Application, IWebApp> _webApps = new();
        private readonly Dictionary<Application, IFunctionApp> _funcApps = new();
        private readonly Dictionary<AzureSQLDatabase, ISqlDatabase> _databases = new();
        private readonly Dictionary<AzureServiceBusQueue, IQueue> _serviceQueues = new();
        private readonly Dictionary<AzureTableStorage, IStorageAccount> _tableStorages = new();
        private readonly Dictionary<Application, AzureAppService> _appServices = new();
        private readonly Dictionary<Application, AzureFunctionApp> _functionApps = new();
        private readonly Dictionary<AzureAppService, IWebApp> _webAppList = new();
        private readonly Dictionary<AzureFunctionApp, IFunctionApp> _functionAppList = new();

        private bool _initialized;

        /// <summary>
        /// Constructor
        /// </summary>
        public AzureDeploymentManager() : base("Azure")
        {

        }

        /// <summary>
        /// Lists resources that can be prepared with this instance.
        /// </summary>
        /// <returns>List of resources that can be prepared with this instance.</returns>
        public override List<System.Type> GetSupportedResources()
            =>
                new List<System.Type>
                {
                    typeof (AzureSQLDatabase),
                    typeof (AzureTableStorage),
                    typeof (AzureServiceBusQueue),
                    typeof (AzureAppService),
                    typeof (AzureFunctionApp)
                };

        /// <summary>
        /// Lists applications that can be prepared with this instance.
        /// </summary>
        /// <returns>List of applications that can be prepared with this instance.</returns>
        public override List<System.Type> GetSupportedApplications() =>
                new List<System.Type>
                {
                    typeof (RestApiApplication),
                    typeof (WorkerApplication)
                };

        /// <summary>
        /// Prepares set of applications and store them into PreparedApplications.
        /// </summary>
        /// <param name="applications">Applications to be prepared.</param>
        public override void PrepareApplications(List<Application> applications)
        {
            foreach (var app in applications)
            {
                if (!_webAppList.FirstOrDefault(x => x.Key.WithApplication == app.Name).Equals(default(KeyValuePair<AzureAppService, IWebApp>)))
                {
                    _appServices.Add(app, _webAppList.Single(x => x.Key.WithApplication == app.Name).Key);
                    _webApps.Add(app, _webAppList.Single(x => x.Key.WithApplication == app.Name).Value);
                    PreparedApplications.Add(app);
                }
                else
                {
                    _functionApps.Add(app, _functionAppList.Single(x => x.Key.WithApplication == app.Name).Key);
                    _funcApps.Add(app, _functionAppList.Single(x => x.Key.WithApplication == app.Name).Value);
                    PreparedApplications.Add(app);
                }

            }
        }

        /// <summary>
        /// Deploys set of applications and store them into DeployedApplications.
        /// </summary>
        /// <param name="applications">Applications to be deployed.</param>
        public override void DeployApplications(List<Application> applications)
        {
            Init();

            foreach (var application in applications)
            {
                if (PreparedApplications.Contains(application))
                {
                    Deploy((dynamic)application);
                }
            }
        }

        /// <summary>
        /// Deallocates all resouces and applications.
        /// </summary>
        public override void Clear()
        {
            Console.WriteLine("Deleting resource group");
            _azure.ResourceGroups.DeleteByName(_resourceGroup.Name);
        }

        /// <summary>
        /// Prepares set of applications and store them into PreparedApplications.
        /// </summary>  
        /// <param name="resources">Applications to be prepared.</param>
        public override void PrepareResources(List<Resource> resources)
        {
            Init();
            foreach (var resource in resources)
            {
                PrepareResource((dynamic)resource);
            }
        }
        private void Deploy(Application application)
        {
            // Not supported
        }

        private void Deploy(WorkerApplication application)
        {
            bool isServerless = false;
            IPublishingProfile publishProfile;

            if (_webApps.ContainsKey(application))
            {
                publishProfile = _webApps[application].GetPublishingProfile();
            }
            else
            {
                isServerless = true;
                publishProfile = _funcApps[application].GetPublishingProfile();
                // Add connection strings to function app
                _funcApps[application].Update().WithAppSettings(_queueConnStr).Apply();
            }

            FtpClient client = new FtpClient();
            var url = publishProfile.FtpUrl;

            Uri myUri = new Uri("https://" + url);
            var ip = Dns.GetHostAddresses(myUri.Host)[0];
            client.Host = ip.ToString();

            client.Credentials = new NetworkCredential(publishProfile.FtpUsername, publishProfile.FtpPassword);
            var files =
                Directory.GetFiles(
                    Path.Combine(ConfigProvider.GetValue("OutputFolderPath"), application.Name, "build"), "*",
                    SearchOption.AllDirectories);

            client.Connect();

            foreach (var file in files)
            {
                Uri fullPath = new Uri(file, UriKind.Absolute);
                Uri relRoot = new Uri(Path.Combine(ConfigProvider.GetValue("OutputFolderPath"), application.Name, "build"), UriKind.Absolute);

                string relPath = relRoot.MakeRelativeUri(fullPath).ToString();

                if (isServerless)
                {
                    client.UploadFile(file, @"\site\wwwroot" + "\\" + relPath.Substring(6), FtpRemoteExists.Overwrite, true);
                }
                else
                {
                    client.UploadFile(file, (@"\site\wwwroot\App_Data\Jobs\continuous\Worker" + "\\" + relPath.Substring(6)), FtpRemoteExists.Overwrite, true);
                    if (Path.GetFileName(file.ToLower()).Contains("packages.config"))
                    {
                        client.UploadFile(file, (@"\site\wwwroot" + "\\" + relPath.Substring(6)), FtpRemoteExists.Overwrite, true);
                    }
                }
            }

            if (!isServerless)
            {
                _webApps[application].Refresh();
            }

            DeployedApplications.Add(application);
            client.Dispose();
        }

        private void Deploy(RestApiApplication application)
        {
            bool isServerless = false;
            IPublishingProfile publishProfile;

            if (_webApps.ContainsKey(application))
            {
                publishProfile = _webApps[application].GetPublishingProfile();
            }
            else
            {
                isServerless = true;
                publishProfile = _funcApps[application].GetPublishingProfile();
            }

            FtpClient client = new FtpClient();
            var url = publishProfile.FtpUrl;

            Uri myUri = new Uri("https://" + url);
            var ip = Dns.GetHostAddresses(myUri.Host)[0];
            client.Host = ip.ToString();

            client.Credentials = new NetworkCredential(publishProfile.FtpUsername, publishProfile.FtpPassword);
            var files =
                Directory.GetFiles(
                    Path.Combine(ConfigProvider.GetValue("OutputFolderPath"), application.Name, "build"), "*",
                    SearchOption.AllDirectories);
            
            client.Connect();

            foreach (var file in files)
            {
                Uri fullPath = new Uri(file, UriKind.Absolute);
                Uri relRoot = new Uri(Path.Combine(ConfigProvider.GetValue("OutputFolderPath"), application.Name, "build"), UriKind.Absolute);

                string relPath = relRoot.MakeRelativeUri(fullPath).ToString();
                
                if (isServerless)
                {
                    client.UploadFile(file, @"\site\wwwroot" + "\\" + relPath.Substring(6), FtpRemoteExists.Overwrite, true);
                }
                else
                {
                    if (Path.GetFileName(file.ToLower()).Contains("global.asax") ||
                        Path.GetFileName(file.ToLower()).Contains("web.config") ||
                        Path.GetFileName(file.ToLower()).Contains("packages.config"))
                    {
                        client.UploadFile(file, (@"\site\wwwroot" + "\\" + relPath.Substring(6)), FtpRemoteExists.Overwrite, true);
                    }
                    else
                    {
                        client.UploadFile(file, (@"\site\wwwroot\bin" + "\\" + relPath.Substring(6)), FtpRemoteExists.Overwrite, true);
                    }
                }               
            }

            if (_webApps.ContainsKey(application))
            {
                application.BaseUrl = _webApps[application].DefaultHostName;
            }
            else
            {
                application.BaseUrl = _funcApps[application].DefaultHostName;
            }

            DeployedApplications.Add(application);
            client.Dispose();
        }

        private void PrepareResource(Resource resource)
        {
            // Not supported
        }

        private void PrepareResource(AzureAppService resource)
        {
            Init();
            var tier = GetPricingTierFromString(resource.PerformanceTier);

            Console.WriteLine("Making app service: " + resource.Name);
            var app = _azure.WebApps
                .Define(Guid.NewGuid().ToString("N").Substring(0, 8))
                .WithRegion(ConfigProvider.GetValue("AzureRegion"))
                .WithExistingResourceGroup(_resourceGroup.Name)
                .WithNewWindowsPlan(tier)
                .Create();
            PreparedResources.Add(resource);
            Console.WriteLine("Done: " + resource.Name);


            _webAppList.Add(resource, app);
        }
        private void PrepareResource(AzureSQLDatabase resource)
        {
            var serverLogin = "sqladmin3423";
            var serverPass = "myS3cureP@ssword";
            if (_sqlServer == null)
            {
                Console.WriteLine("Making sql server");
                _sqlServer = _azure.SqlServers.Define(Guid.NewGuid().ToString("N").Substring(0, 16))
                        .WithRegion(ConfigProvider.GetValue("AzureRegion"))
                        .WithExistingResourceGroup(_resourceGroup)
                        .WithAdministratorLogin("sqladmin3423")
                        .WithAdministratorPassword("myS3cureP@ssword")
                        .WithNewFirewallRule("0.0.0.0", "255.255.255.255", "shady")
                        .Create();
            }
            Console.WriteLine("Making sql db: " + resource.Name);

            DatabaseEdition edition = DatabaseEdition.Free;
            switch (resource.PerformanceTier.ToLower())
            {
                case "basic":
                    edition = DatabaseEdition.Basic;
                    break;
                case "premium":
                    edition = DatabaseEdition.Premium;
                    break;
                case "free":
                    edition = DatabaseEdition.Free;
                    break;
                case "standard":
                    edition = DatabaseEdition.Standard;
                    break;              
            }

            ServiceObjectiveName serviceObjective = ServiceObjectiveName.Free;
            switch (resource.ServiceObjective.ToLower())
            {
                case "basic":
                    serviceObjective = ServiceObjectiveName.Basic;
                    break;
                case "free":
                    serviceObjective = ServiceObjectiveName.Free;
                    break;
                case "p1":
                    serviceObjective = ServiceObjectiveName.P1;
                    break;
                case "p2":
                    serviceObjective = ServiceObjectiveName.P2;
                    break;
                case "p3":
                    serviceObjective = ServiceObjectiveName.P3;
                    break;
                case "p4":
                    serviceObjective = ServiceObjectiveName.P4;
                    break;
                case "p6":
                    serviceObjective = ServiceObjectiveName.P6;
                    break;
                case "p11":
                    serviceObjective = ServiceObjectiveName.P11;
                    break;
                case "p15":
                    serviceObjective = ServiceObjectiveName.P15;
                    break;
                case "s0":
                    serviceObjective = ServiceObjectiveName.S0;
                    break;
                case "s1":
                    serviceObjective = ServiceObjectiveName.S1;
                    break;
                case "s2":
                    serviceObjective = ServiceObjectiveName.S2;
                    break;
                case "s3":
                    serviceObjective = ServiceObjectiveName.S3;
                    break;
                case "s4":
                    serviceObjective = ServiceObjectiveName.S4;
                    break;
                case "s6":
                    serviceObjective = ServiceObjectiveName.S6;
                    break;
                case "s7":
                    serviceObjective = ServiceObjectiveName.S7;
                    break;
                case "s9":
                    serviceObjective = ServiceObjectiveName.S9;
                    break;
                case "s12":
                    serviceObjective = ServiceObjectiveName.S12;
                    break;
            }

            _databases.Add(resource, _sqlServer.Databases.Define(resource.Name)
                .WithEdition(edition).WithServiceObjective(serviceObjective)
                        .Create());

            PreparedResources.Add(resource);
            resource.ConnectionString = "Server=tcp:" + _sqlServer.Name + ".database.windows.net,1433;Initial Catalog=" +
                                        _databases[resource].Name + ";Persist Security Info=False;User ID=" +
                                        serverLogin + ";Password=" + serverPass +
                                        ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            Console.WriteLine("Done: " + resource.Name);
        }
        private void PrepareResource(AzureServiceBusQueue resource)
        {

            Console.WriteLine("Making AzureServiceBusQueue: " + resource.Name);
            var queueName = resource.Name;
            var serviceBusNamespace = _azure.ServiceBusNamespaces
                .Define(SdkContext.RandomResourceName("namespace",20))
                .WithRegion(_resourceGroup.RegionName)
                .WithExistingResourceGroup(_resourceGroup)
                .WithNewQueue(queueName, resource.SizeInGB*1024)
                .Create();

            var firstQueue = serviceBusNamespace.Queues.GetByName(queueName);
            var rule = firstQueue.AuthorizationRules.Define("rulerule").WithManagementEnabled().Create();
            
            resource.ConnectionString = rule.GetKeys().PrimaryConnectionString;
            _queueConnStr.Add(resource.Name + "Connection", resource.ConnectionString.Substring(0, resource.ConnectionString.IndexOf("EntityPath=")));

            PreparedResources.Add(resource);
            _serviceQueues.Add(resource, firstQueue);                                

            Console.WriteLine("Done: " + resource.Name);
        }
        private void PrepareResource(AzureTableStorage resource)
        {
            Console.WriteLine("Making AzureTableStorage: " + resource.Name);

            if (_storageAccount == null)
            {
                CreateStorageAccount();
            }

            PreparedResources.Add(resource);
            _tableStorages.Add(resource, _storageAccount);
            resource.ConnectionString = "DefaultEndpointsProtocol=https;AccountName=" + _storageAccount.Name +
                                        ";AccountKey=" +
                                        _storageAccount.GetKeys().First().Value +
                                        ";EndpointSuffix=core.windows.net";

            Console.WriteLine("Done: " + resource.Name);
        }

        private void PrepareResource(AzureFunctionApp resource)
        {
            Console.WriteLine("Making function app: " + resource.Name);
            IFunctionApp app;

            if (_storageAccount == null)
            {
                CreateStorageAccount();
            }

            if (resource.PlanName.ToLower() == "dedicated")
            {
                var tier = GetPricingTierFromString(resource.PerformanceTier);

                app = _azure.AppServices.FunctionApps
                    .Define(Guid.NewGuid().ToString("N").Substring(0, 8))
                    .WithRegion(ConfigProvider.GetValue("AzureRegion"))
                    .WithExistingResourceGroup(_resourceGroup.Name)
                    .WithNewAppServicePlan(tier)
                    .WithLatestRuntimeVersion()
                    .WithExistingStorageAccount(_storageAccount)
                    .Create();
            }
            else
            {
                app = _azure.AppServices.FunctionApps
                    .Define(Guid.NewGuid().ToString("N").Substring(0, 8))
                    .WithRegion(ConfigProvider.GetValue("AzureRegion"))
                    .WithExistingResourceGroup(_resourceGroup.Name)
                    .WithNewConsumptionPlan()
                    .WithLatestRuntimeVersion()
                    .WithExistingStorageAccount(_storageAccount)
                    .Create();
            }

            PreparedResources.Add(resource);
            Console.WriteLine("Done: " + resource.Name);

            _functionAppList.Add(resource, app);
        }

        private void CreateStorageAccount()
        {
            _storageAccount = _azure.StorageAccounts.Define(Guid.NewGuid().ToString("N").Substring(0, 16))
                .WithRegion(_resourceGroup.RegionName)
                .WithExistingResourceGroup(_resourceGroup)
                .Create();
        }

        private void Init()
        {
            if (!_initialized)
            {
                Console.WriteLine("Azure authentization");
                var azureRegion = ConfigProvider.TryGetValue("AzureRegion");
                _azure =
                    Microsoft.Azure.Management.Fluent.Azure.Authenticate(new AzureCredentials(
                        new ServicePrincipalLoginInformation { ClientId = ConfigProvider.GetValue("ClientId"), ClientSecret = ConfigProvider.GetValue("ClientSecret") }, ConfigProvider.GetValue("TennantId"),
                        AzureEnvironment.AzureGlobalCloud)).WithSubscription(ConfigProvider.GetValue("SubscriptionId"));
                Console.WriteLine("Making resource group");
                _resourceGroup = _azure.ResourceGroups
                        .Define(ConfigProvider.TryGetValue("ResourceGroupName") ?? "CloudPrototyperGroup")
                        .WithRegion(azureRegion ?? "westeurope")
                        .Create();

                _initialized = true;
            }
        }

        private PricingTier GetPricingTierFromString(string tier)
        {
            switch (tier.ToLower())
            {
                case "freef1":
                    return PricingTier.FreeF1;
                case "basicb1":
                    return PricingTier.BasicB1;
                case "basicb2":
                    return PricingTier.BasicB2;
                case "basicb3":
                    return PricingTier.BasicB3;
                case "premiump1":
                    return PricingTier.PremiumP1;
                case "premiump2":
                    return PricingTier.PremiumP2;
                case "premiump3":
                    return PricingTier.PremiumP3;
                case "sharedd1":
                    return PricingTier.SharedD1;
                case "standards1":
                    return PricingTier.StandardS1;
                case "standards2":
                    return PricingTier.StandardS2;
                case "standards3":
                    return PricingTier.StandardS3;
                default:
                    return PricingTier.FreeF1;
            }
        }
    }
}
