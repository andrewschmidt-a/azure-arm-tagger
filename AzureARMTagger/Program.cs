
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AzureARMTagger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string DirPath = Environment.GetEnvironmentVariable("pathToFiles")??"./";
            

            // Read the template and parameter file contents
            JObject templateFileContents = GetJsonFileContents(DirPath+Environment.GetEnvironmentVariable("pathToTemplateFile"));
            
            // Read the tags
            JObject tagFileContents = GetJsonFileContents(DirPath+Environment.GetEnvironmentVariable("pathToTagFile"));
            // Add Tags
            templateFileContents = AddTagsToTemplate(templateFileContents, tagFileContents);
        }

        /// <summary>
        /// Reads a JSON file from the specified path
        /// </summary>
        /// <param name="pathToJson">The full path to the JSON file</param>
        /// <returns>The JSON file contents</returns>
        static private JObject GetJsonFileContents(string pathToJson)
        {
            JObject templatefileContent = new JObject();
            using (StreamReader file = File.OpenText(pathToJson))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    templatefileContent = (JObject)JToken.ReadFrom(reader);
                    return templatefileContent;
                }
            }
        }

        /// <summary>
        /// Iterates through resources in a template and adds tags to the resources
        /// </summary>
        /// <param name="template">JObject of the template</param>
        /// <param name="tags">JObject of the tags</param>
        /// <returns>The JSON Object with tags inserted</returns>
        static private JObject AddTagsToTemplate(JObject template, JObject tags){
            foreach(JObject resource in template["resources"]){
                foreach(var tag in tags){
                    resource["tags"][tag.Key] = tag.Value;
                }
            }
            return template;
        }
}
    }
