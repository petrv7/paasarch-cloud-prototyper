using System;
using System.IO;
using CloudPrototyper.Examples;
using CloudPrototyper.Model;
using Newtonsoft.Json;

namespace CloudPrototyper.Console
{
    /// <summary>
    /// Entry point of application. Triggers all steps of prototype of life cycle.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point of application. Triggers all steps of prototype of life cycle.
        /// Loads JSON, creates ModelResolver instatnce and calls its methods.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            
            var json = string.Empty;
            System.Console.WriteLine("Type path to config file:");
            var configFilePath = System.Console.ReadLine();

            if (string.IsNullOrEmpty(configFilePath))
            {
                System.Console.WriteLine("Configuration file path is required!");
                System.Console.ReadKey();
                return;
            }

            System.Console.WriteLine("Type path to file with model in JSON format");
            var modelJsonFilePath = System.Console.ReadLine();

            if (!string.IsNullOrEmpty(modelJsonFilePath))
            {
                json = File.ReadAllText(modelJsonFilePath);
            }
            else
            {
                System.Console.WriteLine("No model was provided. Type 1 for Social Network Sample or type 2 for Serverless Sample.");
                var option = System.Console.ReadKey();
                System.Console.WriteLine();

                switch (option.KeyChar)
                {
                    case '1':
                        modelJsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SocialNetworkModel.json");
                        System.Console.WriteLine("Social network sample is used.");
                        break;
                    case '2':
                        modelJsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ServerlessModel.json");
                        System.Console.WriteLine("Serverless sample is used.");
                        break;
                    default:
                        System.Console.WriteLine("Invalid input.");
                        return;
                }

                json = File.ReadAllText(modelJsonFilePath);
            }

            var prototype = JsonConvert.DeserializeObject<Prototype>(json, settings);
            var prototypeManager = new ModelResolver.Implementations.ModelResolver(prototype, configFilePath);

            System.Console.WriteLine("Verifying model");
            prototypeManager.VerifyModel();
            System.Console.WriteLine("Model is OK");
            
            System.Console.WriteLine("Preparing resources");
            prototypeManager.PrepareResources();
            System.Console.WriteLine("Prepared"); 

            System.Console.WriteLine("Generating prototype");
            prototypeManager.Generate();
            System.Console.WriteLine("Generated");
            
            System.Console.WriteLine("Deploying");
            prototypeManager.Deploy();
            System.Console.WriteLine("Deployed");
            
            System.Console.WriteLine("Generating benchmark");
            prototypeManager.GenerateBenchmark();
            System.Console.WriteLine("Done");
            
            //Automated cleanup is not required. User has to delete resource group and output files on his own.
            /* System.Console.WriteLine("Cleaning");
             prototypeManager.CleanUp();
             System.Console.WriteLine("Cleaned");*/

            System.Console.ReadKey();
        }
    }
}
