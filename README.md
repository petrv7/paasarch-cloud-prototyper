# PaaSArch Cloud Prototyper Tool

### Dependencies
|                 | Required .NET version          |
|-----------------|-----------------------|
| Tool            | .NET 6                |
| PaaS prototypes | .NET Framework 4.6.2  |
| FaaS prototypes | .NET 6 + .NET Core 3.1 |

[dotnet-t4](https://www.nuget.org/packages/dotnet-t4/)

### Install
```
git clone https://github.com/petrv7/paasarch-cloud-prototyper
cd paasarch-cloud-prototyper/src
dotnet build CloudPrototyper.Console/CloudPrototyper.Console.csproj -o build
cd build
./CloudPrototyper.Console.exe
```

### Configuration
Tool requires configuration file in order to create resources in Azure. The ```TennantId``` and ```SubscriptionId``` can be found in your Azure subscription settings. To get the ```ClientId``` and ```ClientSecret``` you need to register service application in the Active Directory with sufficient permissions to manipulate with resources.
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
	</appSettings>
</configuration>
```

### Authors 
Original authors: Ondřej Gasior and David Gešvindr<br />
Serverless extension: [Petr Vitovský](https://github.com/petrv7)
