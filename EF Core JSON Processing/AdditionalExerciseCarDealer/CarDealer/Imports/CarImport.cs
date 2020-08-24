namespace CarDealer.Imports
{
    using Newtonsoft.Json;
    using System;
    
    using Imports.FileProcessing;

    using CarDealer.Models;

    public class CarImport : JsonProcess
    {
        public CarImport(string fullPath)
            : base(fullPath)
        {
        }

        public override void Import()
        {
            var cars = JsonConvert
               .DeserializeObject<Car[]>(this.LoadedFile);

            this.Context.Cars.AddRange(cars);

            this.Context.SaveChanges();

            Console.WriteLine($"Successfully cars imported {cars.Length}");
        }
    }
}
