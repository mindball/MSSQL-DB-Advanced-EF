namespace CarDealer.Models
{
    using System;

    using Newtonsoft.Json;

    using CarDealer.Data;
    using CarDealer.Models.Contracts;

    public class CarImport : IImporter
    {
        public void Import(CarDealerContext context, string fileName)
        {
            var cars = JsonConvert
               .DeserializeObject<Car[]>(fileName);

            context.Cars.AddRange(cars);
            Console.WriteLine($"Successfully imported {cars.Length}");
        }
    }
}
