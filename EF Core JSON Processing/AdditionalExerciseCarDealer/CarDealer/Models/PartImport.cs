namespace CarDealer.Models
{
    using System;

    using Newtonsoft.Json;

    using CarDealer.Data;
    using CarDealer.Models.Contracts;

    public class PartImport : IImporter
    {
        public void Import(CarDealerContext context, string fileName)
        {
            var parts = JsonConvert
              .DeserializeObject<Part[]>(fileName);

            context.Parts.AddRange(parts);
            Console.WriteLine($"Successfully imported {parts.Length}");
        }
    }
}
