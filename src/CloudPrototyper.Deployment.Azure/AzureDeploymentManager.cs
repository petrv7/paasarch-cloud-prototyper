﻿using System;
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
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Management.Eventhub.Fluent;
using Microsoft.Azure.Management.EventHub.Fluent.Models;
using Azure.ResourceManager.CosmosDB;
using Azure.Identity;
using Azure.ResourceManager.CosmosDB.Models;
using CloudPrototyper.NET.v6.Functions.Model;
using CloudPrototyper.NET.Standard.v20.CosmosDb.Model;
using CloudPrototyper.NET.Standard.v20.EventHub.Model;
using Sku = Microsoft.Azure.Management.EventHub.Fluent.Models.Sku;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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
        private static readonly HttpClient client = new();

        private IAzure _azure;
        private IResourceGroup _resourceGroup;
        private ISqlServer _sqlServer;

        private CosmosDBManagementClient _cosmosManagementClient;
        private CosmosClient _cosmosClient;
        private CosmosClient _cosmosClientServerless;
        private Database _cosmosDatabase;
        private Database _cosmosDatabaseServerless;
        private string _cosmosConnectionString;
        private string _cosmosConnectionStringServerless;

        private IStorageAccount _storageAccount;
        private readonly Dictionary<AzureCosmosDbContainer, Container> _containers = new Dictionary<AzureCosmosDbContainer, Container>();
        private readonly Dictionary<string, string> _connStr = new();
        private readonly Dictionary<Application, IWebApp> _webApps = new();
        private readonly Dictionary<Application, IFunctionApp> _funcApps = new();
        private readonly Dictionary<AzureSQLDatabase, ISqlDatabase> _databases = new();
        private readonly Dictionary<AzureServiceBusQueue, IQueue> _serviceQueues = new();
        private readonly Dictionary<AzureEventHub, IEventHub> _eventHubs = new();
        private readonly Dictionary<AzureEventHubNamespace, IEventHubNamespace> _eventHubNamespaces = new();
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
                    typeof (AzureCosmosDbContainer),
                    typeof (AzureFunctionApp),
                    typeof (AzureEventHub),
                    typeof (AzureEventHubNamespace)
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
            string url = "";

            foreach (var app in applications)
            {
                if (!_webAppList.FirstOrDefault(x => x.Key.WithApplication == app.Name).Equals(default(KeyValuePair<AzureAppService, IWebApp>)))
                {
                    _appServices.Add(app, _webAppList.Single(x => x.Key.WithApplication == app.Name).Key);
                    _webApps.Add(app, _webAppList.Single(x => x.Key.WithApplication == app.Name).Value);
                    PreparedApplications.Add(app);
                    url = _webApps[app].DefaultHostName;
                }
                else
                {
                    _functionApps.Add(app, _functionAppList.Single(x => x.Key.WithApplication == app.Name).Key);
                    _funcApps.Add(app, _functionAppList.Single(x => x.Key.WithApplication == app.Name).Value);
                    PreparedApplications.Add(app);
                    url = _funcApps[app].DefaultHostName;
                }

                if (app is RestApiApplication restApp)
                {
                    restApp.BaseUrl = url;
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

            // We need to prepare event hub namespaces and cosmos accounts first
            var eventHubNamespaces = resources.OfType<AzureEventHubNamespace>();
            foreach (var resource in eventHubNamespaces)
            {
                PrepareResource((dynamic)resource);
            }

            if (resources.OfType<AzureCosmosDbContainer>().Any(c => !c.IsServerless))
            {
                PrepareAzureCosmosDbAccount(false);
            }

            if (resources.OfType<AzureCosmosDbContainer>().Any(c => c.IsServerless))
            {
                PrepareAzureCosmosDbAccount(true);
            }

            foreach (var resource in resources.Except(eventHubNamespaces))
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
            var isServerless = !_webApps.ContainsKey(application);
            IPublishingProfile publishProfile;

            if (!isServerless)
            {
                publishProfile = _webApps[application].GetPublishingProfile();
            }
            else
            {
                publishProfile = _funcApps[application].GetPublishingProfile();
                // Add connection strings to function app
                _funcApps[application].Update().WithAppSettings(_connStr).Apply();
            }

            var client = new FtpClient();
            var url = publishProfile.FtpUrl;

            var myUri = new Uri("https://" + url);
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
                var fullPath = new Uri(file, UriKind.Absolute);
                var relRoot = new Uri(Path.Combine(ConfigProvider.GetValue("OutputFolderPath"), application.Name, "build"), UriKind.Absolute);

                var relPath = relRoot.MakeRelativeUri(fullPath).ToString();

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
            var isServerless = !_webApps.ContainsKey(application);

            var publishProfile = isServerless ? _funcApps[application].GetPublishingProfile() : _webApps[application].GetPublishingProfile();

            var client = new FtpClient();
            var url = publishProfile.FtpUrl;

            var myUri = new Uri("https://" + url);
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
                var fullPath = new Uri(file, UriKind.Absolute);
                var relRoot = new Uri(Path.Combine(ConfigProvider.GetValue("OutputFolderPath"), application.Name, "build"), UriKind.Absolute);

                var relPath = relRoot.MakeRelativeUri(fullPath).ToString();
                
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
                        .WithAdministratorLogin(serverLogin)
                        .WithAdministratorPassword(serverPass)
                        .WithNewFirewallRule("0.0.0.0", "255.255.255.255", "shady")
                        .Create();
            }
            Console.WriteLine("Making sql db: " + resource.Name);

            DatabaseEdition edition;
            ServiceObjectiveName serviceObjective;

            if (resource.PerformanceTier.ToLower() == "serverless")
            {
                edition = DatabaseEdition.Parse("GeneralPurpose");
                serviceObjective = ServiceObjectiveName.Parse("GP_S_Gen5_" + resource.MaxvCores);
            }
            else
            {
                edition = DatabaseEdition.Parse(resource.PerformanceTier);
                serviceObjective = ServiceObjectiveName.Parse(resource.ServiceObjective);
            }

            _databases.Add(resource, _sqlServer.Databases.Define(resource.Name)
                .WithEdition(edition).WithServiceObjective(serviceObjective)
                        .Create());

            PreparedResources.Add(resource);
            resource.ConnectionString = "Server=tcp:" + _sqlServer.Name + ".database.windows.net,1433;Initial Catalog=" +
                                        _databases[resource].Name + ";Persist Security Info=False;User ID=" +
                                        serverLogin + ";Password=" + serverPass +
                                        ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            // update serverless database autopause delay and min vcores
            if (resource.PerformanceTier.ToLower() == "serverless")
            {
                SetAuthToken();
                HttpRequestMessage request = new(HttpMethod.Put, @"https://management.azure.com/subscriptions/" + ConfigProvider.GetValue("SubscriptionId") + "/resourceGroups/" + _resourceGroup.Name + "/providers/Microsoft.Sql/servers/" + _sqlServer.Name + "/databases/" + resource.Name + "?api-version=2021-02-01-preview");

                request.Content = new StringContent($"{{\"location\": \"{ConfigProvider.GetValue("AzureRegion")}\", \"properties\": {{ \"autoPauseDelay\": {resource.AutopauseDelay}, \"minCapacity\": {resource.MinvCores.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)} }}}}", Encoding.UTF8, "application/json");
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
            }

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
            _connStr.Add(resource.Name + "Connection", resource.ConnectionString.Substring(0, resource.ConnectionString.IndexOf("EntityPath=")));

            PreparedResources.Add(resource);
            _serviceQueues.Add(resource, firstQueue);                                

            Console.WriteLine("Done: " + resource.Name);
        }

        private void PrepareResource(AzureEventHubNamespace resource)
        {
            Console.WriteLine("Making EventHub namespace: " + resource.Name);

            var skuName = resource.PricingTier.ToLower() switch
            {
                "basic" => SkuName.Basic,
                "standard" => SkuName.Standard,
                _ => SkuName.Basic
            };

            var sku = new EventHubNamespaceSkuType(new Sku(skuName));

            var eventHubNamespaceCreation = _azure.EventHubNamespaces
                .Define(resource.Name)
                .WithRegion(_resourceGroup.RegionName)
                .WithExistingResourceGroup(_resourceGroup)
                .WithSku(sku)
                .WithCurrentThroughputUnits(resource.ThroughputUnits);

            if (resource.WithAutoScale)
            {
                eventHubNamespaceCreation = eventHubNamespaceCreation
                    .WithAutoScaling()
                    .WithThroughputUnitsUpperLimit(resource.MaxThroughputUnits);
            }

            var eventHubNamespace = eventHubNamespaceCreation.Create();

            _eventHubNamespaces.Add(resource, eventHubNamespace);
            PreparedResources.Add(resource);

            Console.WriteLine("Done: " + resource.Name);
        }

        private void PrepareResource(AzureEventHub resource)
        {
            var eventHubNamespace = _eventHubNamespaces.FirstOrDefault(k => k.Key.Name == resource.WithNamespace).Value;

            if (eventHubNamespace == null)
            {
                throw new ArgumentException("Azure EventHub " + resource.Name + " is missing EventHub namespace!");
            }

            Console.WriteLine("Making EventHub: " + resource.Name);

            var hub = _azure.EventHubs
                .Define(resource.Name)
                .WithExistingNamespace(eventHubNamespace)
                .WithPartitionCount(resource.PartitionCount)
                .WithRetentionPeriodInDays(1)
                .Create();

            _eventHubs.Add(resource, hub);
            resource.ConnectionString = eventHubNamespace.Manager.NamespaceAuthorizationRules.GetByName(_resourceGroup.Name, eventHubNamespace.Name, "RootManageSharedAccessKey").GetKeys().PrimaryConnectionString;
            _connStr.Add(resource.Name + "Connection", resource.ConnectionString);
            PreparedResources.Add(resource);
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

        private void PrepareAzureCosmosDbAccount(bool serverless)
        {
            Console.WriteLine("Making AzureCosmosDB " + (serverless ? "serverless account" : "account"));

            var acc = new DatabaseAccountCreateUpdateParameters(new List<Location>() { new Location() { LocationName = ConfigProvider.GetValue("AzureRegion") } })
                {
                    Location = ConfigProvider.GetValue("AzureRegion")
                };

            var accName = Guid.NewGuid().ToString("N").Substring(0, 16);
            var resourceGroup = ConfigProvider.TryGetValue("ResourceGroupName") ?? "CloudPrototyperGroup";

            if (serverless)
            {
                acc.Capabilities.Add(new Capability() { Name = "EnableServerless" });
            }

            _cosmosManagementClient.DatabaseAccounts.StartCreateOrUpdate(resourceGroup, accName, acc).WaitForCompletionAsync().AsTask().Wait();

            var connStr = _cosmosManagementClient.DatabaseAccounts.ListConnectionStrings(resourceGroup, accName).Value.ConnectionStrings.First().ConnectionString;

            if (serverless)
            {
                _cosmosClientServerless = new CosmosClient(connStr);
                _cosmosConnectionStringServerless = connStr;
                _connStr.Add("serverlessConnStr", connStr);
            }
            else
            {
                _cosmosClient = new CosmosClient(connStr);
                _cosmosConnectionString = connStr;
                _connStr.Add("ConnStr", connStr);
            }

            Console.WriteLine("Done: AzureCosmosDB account");
        }

        private void PrepareResource(AzureCosmosDbContainer resource)
        {
            if (_cosmosDatabase is null && !resource.IsServerless)
            {
                Console.WriteLine("Making AzureCosmosDB Database");
                var createDbTask = _cosmosClient.CreateDatabaseIfNotExistsAsync(Guid.NewGuid().ToString("N").Substring(0, 16));
                createDbTask.Wait();
                _cosmosDatabase = createDbTask.Result.Database;
            }
            else if (_cosmosDatabaseServerless is null && resource.IsServerless)
            {
                Console.WriteLine("Making AzureCosmosDB Serverless Database");
                var createDbTask = _cosmosClientServerless.CreateDatabaseIfNotExistsAsync(Guid.NewGuid().ToString("N").Substring(0, 16));
                createDbTask.Wait();
                _cosmosDatabaseServerless = createDbTask.Result.Database;
            }

            Console.WriteLine("Making AzureCosmosDB Container: " + resource.Name);

            Container container;
            if (resource.IsServerless)
            {
                var createContainerTask = _cosmosDatabaseServerless.CreateContainerIfNotExistsAsync(new ContainerProperties(resource.Name, resource.PartitionKey));
                createContainerTask.Wait();
                container = createContainerTask.Result.Container;

                resource.ConnectionString = _cosmosConnectionStringServerless;
                resource.DatabaseName = _cosmosDatabaseServerless.Id;
            }
            else
            {
                var throughputProperties = resource.ThroughputType == "manual" ?
                ThroughputProperties.CreateManualThroughput(resource.RUs) :
                ThroughputProperties.CreateAutoscaleThroughput(resource.RUs);

                var createContainerTask = _cosmosDatabase.CreateContainerIfNotExistsAsync(new ContainerProperties(resource.Name, resource.PartitionKey), throughputProperties);
                createContainerTask.Wait();
                container = createContainerTask.Result.Container;

                resource.ConnectionString = _cosmosConnectionString;
                resource.DatabaseName = _cosmosDatabase.Id;
            }

            PreparedResources.Add(resource);
            _containers.Add(resource, container);

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

            if (resource.PlanName.ToLower() == "dedicated" || resource.PlanName.ToLower() == "premium")
            {
                var tier = GetPricingTierFromString(resource.PerformanceTier);

                app = _azure.AppServices.FunctionApps
                    .Define(Guid.NewGuid().ToString("N").Substring(0, 8))
                    .WithRegion(ConfigProvider.GetValue("AzureRegion"))
                    .WithExistingResourceGroup(_resourceGroup.Name)
                    .WithNewAppServicePlan(tier)
                    .WithRuntimeVersion("4")
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
                    .WithRuntimeVersion("4")
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
            if (_initialized) return;

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

            var options = new DefaultAzureCredentialOptions
            {
                ManagedIdentityClientId = ConfigProvider.GetValue("ClientId")
            };
            var cred = new DefaultAzureCredential(options);
            _cosmosManagementClient = new CosmosDBManagementClient(ConfigProvider.GetValue("SubscriptionId"), cred);                

            _initialized = true;
        }

        private static PricingTier GetPricingTierFromString(string tier)
        {
            if (tier == null) return PricingTier.FreeF1;

            return tier.ToLower() switch
            {
                "freef1" => PricingTier.FreeF1,
                "basicb1" => PricingTier.BasicB1,
                "basicb2" => PricingTier.BasicB2,
                "basicb3" => PricingTier.BasicB3,
                "premiump1" => PricingTier.PremiumP1,
                "premiump2" => PricingTier.PremiumP2,
                "premiump3" => PricingTier.PremiumP3,
                "sharedd1" => PricingTier.SharedD1,
                "standards1" => PricingTier.StandardS1,
                "standards2" => PricingTier.StandardS2,
                "standards3" => PricingTier.StandardS3,
                "ep1" => new PricingTier("ElasticPremium", "EP1"),
                "ep2" => new PricingTier("ElasticPremium", "EP2"),
                "ep3" => new PricingTier("ElasticPremium", "EP3"),
                _ => PricingTier.FreeF1
            };
        }

        private void SetAuthToken()
        {
            HttpRequestMessage request = new(HttpMethod.Post, @"https://login.microsoftonline.com/" + ConfigProvider.GetValue("TennantId") + "/oauth2/token");
            var content = new Dictionary<string, string>();
            content.Add("client_id", ConfigProvider.GetValue("ClientId"));
            content.Add("client_secret", ConfigProvider.GetValue("ClientSecret"));
            content.Add("grant_type", "client_credentials");
            content.Add("resource", @"https://management.azure.com/");

            request.Content = new FormUrlEncodedContent(content);
            var authResponse = client.Send(request);
            var responseTask = authResponse.Content.ReadAsStringAsync();
            responseTask.Wait();
            dynamic response = JsonConvert.DeserializeObject(responseTask.Result);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)response.access_token);
        }
    }
}
