# PaaSArch Cloud Prototyper Tool

### Install
```
git clone https://github.com/petrv7/paasarch-cloud-prototyper
cd paasarch-cloud-prototyper/src
dotnet build CloudPrototyper.Console/CloudPrototyper.Console.csproj -o build
cd build
./CloudPrototyper.Console.exe
```

### Configuration
Tool requires configuration file in order to create resources in Azure. The ```TennandId``` and ```SubscriptionId``` can be found in your Azure subscription settings. To get the ```ClientId``` and ```ClientSecret``` you need to register service application in the Active Directory with sufficient permissions to manipulate with resources.
```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
	<appSettings>
		<add key="ClientId" value="" />
		<add key="TennantId" value="" />
		<add key="ClientSecret" value="" />
		<add key="SubscriptionId" value="" />
		<add key="ResourceGroupName" value="" />
		<add key="OutputFolderPath" value="" />
		<add key="BenchmarkFileOutput" value="" />
		<add key="DataLayerLibraryPath" value="" />
		<add key="AzureRegion" value="" />
		<add key="CosmosAccount" value="" />
	</appSettings>
</configuration>
```

### Authors 
Original authors: Ondřej Gasior and David Gešvindr
Serverless extension: [Petr Vitovský](https://github.com/petrv7)