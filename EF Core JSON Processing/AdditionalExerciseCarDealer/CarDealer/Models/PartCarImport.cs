

namespace CarDealer.Models
{
    using System;

    using Newtonsoft.Json;

    using CarDealer.Data;
    using CarDealer.Models.Contracts;

    public class PartCarImport : IImporter
    {
        public void Import(CarDealerContext context, string fileName)
        {
            var partsCarts = JsonConvert
               .DeserializeObject<PartCar[]>(fileName);

            context.PartCars.AddRange(partsCarts);

            Console.WriteLine($"Successfully imported {partsCarts.Length}");
        }
    }
}
