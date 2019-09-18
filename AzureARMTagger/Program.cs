
using System.Text.RegularExpressions;
using System.Linq;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace AzureARMTagger
{
    class Program
    {
        static void Main(string[] args)
        {


            if(args.Length < 2 || args[0]== null || args[1] == null){
                throw new Exception("Must Involke with 2 arguments (pathToTemplateFile, pathToTagFile) ");
            }

            string pathToTemplateFile = args[0];
            string pathToTagFile = args[1];

            string DirPath = "./volume/";

            // Read the tags
            JObject tagFileContents = GetJsonFileContents(Path.Combine(DirPath,pathToTagFile));

            pathToTemplateFile = Path.Combine(DirPath,pathToTemplateFile);
            FileAttributes attr = File.GetAttributes(pathToTemplateFile);
            List<string> pathsOfFiles;

            if((attr & FileAttributes.Directory) == FileAttributes.Directory){
                Regex rx = new Regex(@".*\.json");
                pathsOfFiles = Directory.GetFiles(pathToTemplateFile).Where(t=> rx.Match(t).Success).ToList();
            }else{
                pathsOfFiles = new List<string>(){pathToTemplateFile};
            }

            foreach(string path in pathsOfFiles){
                // Read the template and parameter file contents
                JObject templateFileContents = GetJsonFileContents(path);
                
                // Add Tags
                templateFileContents = AddTagsToTemplate(templateFileContents, tagFileContents);

                // write JSON directly to a file
                Directory.CreateDirectory(Path.Combine(DirPath, "output")); // Create in case it doesn't exist
                using (StreamWriter file = File.CreateText(Path.Combine(DirPath, "output",Path.GetFileName(path))))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    writer.Formatting = Formatting.Indented;
                    templateFileContents.WriteTo(writer);
                }
            }
            
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
