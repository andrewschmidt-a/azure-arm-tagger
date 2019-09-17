using System;
using ARMHelpers;

namespace AzureARMTagger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var deploymentHelper = new DeploymentHelper(
                Environment.GetEnvironmentVariable("subscriptionId"), 
                Environment.GetEnvironmentVariable("clientId"), 
                Environment.GetEnvironmentVariable("clientSecret"), 
                Environment.GetEnvironmentVariable("resourceGroupName"), 
                Environment.GetEnvironmentVariable("deploymentName"), 
                Environment.GetEnvironmentVariable("resourceGroupLocation"), 
                Environment.GetEnvironmentVariable("tenantId")
            );
            string DirPath = Environment.GetEnvironmentVariable("pathToFiles")??"./";
            
            deploymentHelper.Run(DirPath+Environment.GetEnvironmentVariable("pathToTemplateFile"), DirPath+Environment.GetEnvironmentVariable("pathToParameterFile"), DirPath+Environment.GetEnvironmentVariable("pathToTagFile"));


            


        }
    }
}
