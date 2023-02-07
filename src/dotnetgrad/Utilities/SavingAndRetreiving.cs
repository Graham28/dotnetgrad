using System;
using System.Data;
using Newtonsoft.Json;

namespace dotnetgrad.Utilities
{
	public static class SavingAndRetreiving
	{
        public static MultiLayerPerceptron GetModelFromJsonFile(string path)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            var model = new MultiLayerPerceptron();
            try
            {
                string json = File.ReadAllText(path);
                model = JsonConvert.DeserializeObject<MultiLayerPerceptron>(json, settings);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return model;
        }

        public static string SaveModelAsJson(MultiLayerPerceptron mlp, string fileName)
        {
            var settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            };
            string json = JsonConvert.SerializeObject(mlp, settings);
            var pathToContentRoot = Directory.GetCurrentDirectory();
            string pathToFolder = "";
            try
            {
                pathToFolder = Directory.GetParent(Directory.GetParent(pathToContentRoot).Parent.FullName).FullName;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Can't find parent of parent directory when trying to save model.");
                Console.WriteLine(e);
            }
            if (!Directory.Exists(pathToFolder + $"/models"))
            {
                Directory.CreateDirectory(pathToFolder + $"/models");
            }
            var pathToFile = pathToFolder + $"/models/{fileName}.json";
            File.WriteAllText(pathToFile, json);
            return pathToFile;
        }
    }
}

