using System;
using System.Globalization;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var weather = new WeatherForeCast();

            DeSerializeFileMicrosoftTextJson("weather");
            SerializeFileMicrosoftTextJson(weather);

            DeSerializeFileNewtonsoftJson("weather");
            SerializeFileNewtonsoftJson(weather);

            //Configuring JSON.NET
            FormatJSON(weather);
            DeserializeAnonymousType("weather");
            ParsingОfObjects(weather);

        }

        private static void ParsingОfObjects(WeatherForeCast weather)
        {
            //List of settings
            var jsonSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented,
                Culture = CultureInfo.InvariantCulture
                //and so on....
            };

            Console.WriteLine(JsonConvert.SerializeObject(weather, jsonSettings));
        }

        private static void DeserializeAnonymousType(string fileName)
        {
            var partOfData = new { TempetureC = 0, Summary = string.Empty };

            var file = File.ReadAllText(fileName);

            partOfData = JsonConvert.DeserializeAnonymousType(file, partOfData);

            Console.WriteLine($"{partOfData.TempetureC} {partOfData.Summary}");

        }

        private static void FormatJSON(WeatherForeCast weather)
        {
            //Human readable
            Console.WriteLine(JsonConvert.SerializeObject(weather, Formatting.Indented));
        }

        private static void SerializeFileNewtonsoftJson(WeatherForeCast weather)
        {
            var convertToJson = JsonConvert.SerializeObject(weather);

            File.WriteAllText("weather", convertToJson);
        }

        private static void DeSerializeFileNewtonsoftJson(string fileName)
        {
            var file = File.ReadAllText(fileName);
            var weather = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherForeCast>(file);

            Console.WriteLine(weather);
        }

        public static void DeSerializeFileMicrosoftTextJson(string fileName)
        {
            var file = File.ReadAllText(fileName);
            var weather = System.Text.Json.JsonSerializer.Deserialize<WeatherForeCast>(file);

            Console.WriteLine(weather);
        }

        //Serializer File
        public static void SerializeFileMicrosoftTextJson(WeatherForeCast weather)
        {
            var convertToJson = System.Text.Json.JsonSerializer.Serialize(weather);

            File.WriteAllText("weather", convertToJson);
        }
    }


}
