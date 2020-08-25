namespace CarDealer.Exports
{
    using System.IO;

    using Newtonsoft.Json;

    using CarDealer.Contracts;

    public class JsonExporter : IExporter
    {
        public void Export<T>(string filePath, T[] collection)
        {
            var jsonString = JsonConvert.SerializeObject(collection);
            File.WriteAllText(filePath, jsonString);
        }

        public void Export<T>(string filePath, T model)
        {
            var jsonString = JsonConvert.SerializeObject(model);
            File.WriteAllText(filePath, jsonString);
        }

        public string MyProperty { get; set; }
    }
}
