using System.Collections.Generic;
using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.Model;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.Model.Entities;
using CloudPrototyper.Model.Operations;
using CloudPrototyper.Model.Operations.Communication;
using CloudPrototyper.Model.Operations.DataAccess;
using CloudPrototyper.Model.Resources;
using CloudPrototyper.Model.Resources.Storage;
using CloudPrototyper.NET.v6.Functions.Model;
using CloudPrototyper.NET.Framework.v462.Computing.Models;
using CloudPrototyper.NET.Standard.v20.CosmosDb.Model;
using CloudPrototyper.NET.Standard.v20.EventHub.Model;

namespace CloudPrototyper.Examples
{
    public class ServerlessSample
    {
        public Prototype Sample { get; }

        public ServerlessSample()
        {
            Sample = MakeSample();
        }

        private readonly int UserCount = 100;
        private readonly int DataCount = 1000;

        private Prototype MakeSample()
        {
            return new Prototype()
            {
                Applications = new List<Application>()
                {
                    new RestApiApplication()
                    {
                        Name = "UsersMicroservice",
                        Platform = "DotNetCore31",
                        DeployTo = "Azure",
                        Actions = new List<CallableAction>()
                        {
                            new CallableAction()
                            {
                                Name = "GetUserEntity",
                                Operation = new LoadEntitiesFromEntityStorage()
                                {
                                    Name = "GetUserOperation",
                                    EntityName = "UserEntity",
                                    EntitySetName = "Users",
                                    EntityStorageName = "UserContainer",
                                    Filter = new FilterCondition()
                                    {
                                        IsNominal = true,
                                        NumberOfResults = 1,
                                        UseKey = true
                                    }
                                }
                            },
                            new CallableAction()
                            {
                                Name = "GetAllUsers",
                                Operation = new LoadEntitiesFromEntityStorage()
                                {
                                    Name = "GetAllUsersOperation",
                                    EntityName = "UserEntity",
                                    EntitySetName = "Users",
                                    EntityStorageName = "UserContainer",
                                    Filter = new FilterCondition()
                                    {
                                        NumberOfResults = UserCount,
                                        UseKey = true
                                    }
                                }
                            },
                            new CallableAction()
                            {
                                Name = "CreateUserEntity",
                                Operation = new InsertEntityToEntityStorage()
                                {
                                    Name = "CreateUserOperation",
                                    EntityName = "UserEntity",
                                    EntitySetName = "Users",
                                    EntityStorageName = "UserContainer",
                                    NumberOfEntities = 1
                                }
                            },
                            new CallableAction()
                            {
                                Name = "Login",
                                Operation = new SequenceOperation()
                                {
                                    Name = "LoginOperation",
                                    NumberOfRepetitions = 1,
                                    Operations = new List<Operation>()
                                    {
                                        new LoadEntitiesFromEntityStorage()
                                        {
                                            Name = "GetUserForLoginOperation",
                                            EntityName = "UserEntity",
                                            EntitySetName = "Users",
                                            EntityStorageName = "UserContainer",
                                            Filter = new FilterCondition()
                                            {
                                                IsNominal = true,
                                                NumberOfResults = 1,
                                                UseKey = true
                                            }
                                        },
                                        new SimulateComputation()
                                        {
                                            Name = "AuthenticateOperation",
                                            MsLength = 500
                                        }
                                    }
                                }
                            },
                        }
                    },
                    new RestApiApplication()
                    {
                        Name = "DataMicroservice",
                        Platform = "DotNetCore31",
                        DeployTo = "Azure",
                        Actions = new List<CallableAction>()
                        {
                            new CallableAction()
                            {
                                Name = "GetData",
                                Operation = new LoadEntitiesFromEntityStorage()
                                {
                                    Name = "GetDataOperation",
                                    EntityName = "DataEntity",
                                    EntitySetName = "DataSet",
                                    EntityStorageName = "DataContainer",
                                    Filter = new FilterCondition()
                                    {
                                        IsNominal = true,
                                        NumberOfResults = 1,
                                        UseKey = true
                                    }
                                }
                            },
                            new CallableAction()
                            {
                                Name = "GetAllData",
                                Operation = new LoadEntitiesFromEntityStorage()
                                {
                                    Name = "GetAllDataOperation",
                                    EntityName = "DataEntity",
                                    EntitySetName = "DataSet",
                                    EntityStorageName = "DataContainer",
                                    Filter = new FilterCondition()
                                    {
                                        NumberOfResults = DataCount,
                                        UseKey = true
                                    }
                                }
                            },
                            new CallableAction()
                            {
                                Name = "CreateData",
                                Operation = new InsertEntityToEntityStorage()
                                {
                                    Name = "CreateDataOperation",
                                    EntityName = "DataEntity",
                                    EntitySetName = "DataSet",
                                    EntityStorageName = "DataContainer",
                                    NumberOfEntities = 1
                                }
                            },
                        }
                    },
                    new RestApiApplication()
                    {
                        Name = "ApiComposition",
                        Platform = "DotNetCore31",
                        DeployTo = "Azure",
                        Actions = new List<CallableAction>()
                        {
                            new CallableAction()
                            {
                                Name = "LoginAndGetAllData",
                                Operation = new SequenceOperation()
                                {
                                    Name = "LoginAndGetAllDataOperation",
                                    NumberOfRepetitions = 1,
                                    Operations = new List<Operation>()
                                    {
                                        new CallUrlOperation()
                                        {
                                            Name = "LoginOperation",
                                            ResourceName = "UsersMicroservice",
                                            ActionName = "Login"
                                        },
                                        new CallUrlOperation()
                                        {
                                            Name = "GetAllDataOperation",
                                            ResourceName = "DataMicroservice",
                                            ActionName = "GetAllData"
                                        }
                                    }
                                }
                            },
                            new CallableAction()
                            {
                                Name = "LoginAndCreateData",
                                Operation = new SequenceOperation()
                                {
                                    Name = "LoginAndCreateDataAndRequestProcessingOperation",
                                    NumberOfRepetitions = 1,
                                    Operations = new List<Operation>()
                                    {
                                        new CallUrlOperation()
                                        {
                                            Name = "LoginCallOperation",
                                            ResourceName = "UsersMicroservice",
                                            ActionName = "Login"
                                        },
                                        new CallUrlOperation()
                                        {
                                            Name = "CreateDataOperation",
                                            ResourceName = "DataMicroservice",
                                            ActionName = "CreateData"
                                        },
                                        new AddMessageToQueue()
                                        {
                                            Name = "RequestProcessingOperation",
                                            EntityName = "DataEntity",
                                            QueueName = "EventHub"
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new WorkerApplication()
                    {
                        Name = "DataProcessingWorker",
                        Platform = "DotNetCore31",
                        DeployTo = "Azure",
                        Actions = new List<TriggeredAction>()
                        {
                            new TriggeredAction()
                            {
                                Name = "CDC",
                                Trigger = new AzureCosmosDbTrigger()
                                {
                                    ContainerName = "DataContainer",
                                    ProcessOncePerTrigger = false
                                },
                                Operation = new InsertEntityToEntityStorage()
                                {
                                    Name = "InsertDataToSQLOperation",
                                    EntityStorageName = "SQLDatabase",
                                    EntityName = "DataEntity",
                                    EntitySetName = "DataSetSQL",
                                    NumberOfEntities = 1
                                }
                            },
                            new TriggeredAction()
                            {
                                Name = "ProcessData",
                                Trigger = new MessageReceivedTrigger()
                                {
                                    QueueName = "EventHub",
                                    MessageType = "DataEntity"
                                },
                                Operation = new SequenceOperation()
                                {
                                    Name = "ProcessDataOperation",
                                    NumberOfRepetitions = 1,
                                    Operations = new List<Operation>()
                                    {
                                        new SimulateComputation()
                                        {
                                            Name = "ProcessingOperation",
                                            MsLength = 100
                                        },
                                        new InsertEntityToEntityStorage()
                                        {
                                            Name = "CreateLogOperation",
                                            EntityStorageName = "SQLDatabase",
                                            EntityName = "Log",
                                            EntitySetName = "Logs",
                                            NumberOfEntities = 1
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                Entities = new List<Entity>()
                {
                    new Entity()
                    {
                        Name = "UserEntity",
                        Properties = new List<PropertyInfo>
                        {
                            new PropertyInfo
                            {
                                ContentSize = 15,
                                Name = "UserName",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 30,
                                Name = "Password",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 15,
                                Name = "Name",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 50,
                                Name = "Address",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "PhoneNum",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 2,
                                Name = "Age",
                                Type = "System.Int32"
                            }
                        }
                    },
                    new Entity()
                    {
                        Name = "DataEntity",
                        Properties = new List<PropertyInfo>
                        {
                            new PropertyInfo
                            {
                                ContentSize = 200,
                                Name = "RawData",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 50,
                                Name = "Description",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 5,
                                Name = "OriginSystem",
                                Type = "System.Int32"
                            }
                        }
                    },
                    new Entity()
                    {
                        Name = "Log",
                        Properties = new List<PropertyInfo>
                        {
                            new PropertyInfo
                            {
                                ContentSize = 10,
                                Name = "LogName",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 50,
                                Name = "LogInfo",
                                Type = "System.String"
                            }
                        }
                    }
                },
                Resources = new List<Resource>()
                {
                    new AzureFunctionApp()
                    {
                        Name = "UsersMicroserviceApp",
                        DeployTo = "Azure",
                        WithApplication = "UsersMicroservice"
                    },
                    new AzureFunctionApp()
                    {
                        Name = "DataMicroserviceApp",
                        DeployTo = "Azure",
                        WithApplication = "DataMicroservice"
                    },
                    new AzureFunctionApp()
                    {
                        Name = "ApiCompositionApp",
                        DeployTo = "Azure",
                        WithApplication = "ApiComposition"
                    },
                    new AzureFunctionApp()
                    {
                        Name = "DataProcessingWorkerApp",
                        DeployTo = "Azure",
                        WithApplication = "DataProcessingWorker"
                    },
                    new AzureEventHub()
                    {
                        Name = "EventHub",
                        DeployTo = "Azure",
                        PartitionCount = 2,
                        WithNamespace = "PaaSArchEventHubNamespace"
                    },
                    new AzureEventHubNamespace
                    {
                        DeployTo = "Azure",
                        Name = "PaaSArchEventHubNamespace",
                        PricingTier = "standard",
                        WithAutoScale = true,
                        MaxThroughputUnits = 20,
                        ThroughputUnits = 1
                    },
                    new AzureSQLDatabase()
                    {
                        DeployTo = "Azure",
                        Name = "SQLDatabase",
                        PerformanceTier = "serverless",
                        MaxvCores = 8,
                        EntitySets = new List<EntitySet>
                        {
                            new EntitySet()
                            {
                                Name = "Logs",
                                EntityName = "Log",
                                Count = 1
                            },
                            new EntitySet
                            {
                                Count = DataCount + 1,
                                EntityName = "DataEntity",
                                Name = "DataSetSQL"
                            }
                        }
                    },
                    new AzureCosmosDbContainer()
                    {
                        DeployTo = "Azure",
                        Name = "UserContainer",
                        IsServerless = true,
                        EntitySets = new List<EntitySet>
                        {
                            new EntitySet
                            {
                                Count = UserCount + 2,
                                EntityName = "UserEntity",
                                Name = "Users"
                            }
                        },
                        UseGateway = true
                    },
                    new AzureCosmosDbContainer()
                    {
                        DeployTo = "Azure",
                        Name = "DataContainer",
                        IsServerless = true,
                        EntitySets = new List<EntitySet>
                        {
                            new EntitySet
                            {
                                Count = DataCount + 1,
                                EntityName = "DataEntity",
                                Name = "DataSet"
                            }
                        },
                        PartitionKey = "/PartitionKey"
                    }
                }
            };
        }

    }
}
