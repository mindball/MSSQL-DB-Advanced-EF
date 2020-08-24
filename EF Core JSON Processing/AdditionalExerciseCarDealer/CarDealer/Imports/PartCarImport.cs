

namespace CarDealer.Imports
{
    using System;
    using Newtonsoft.Json;
    
    using CarDealer.Imports.FileProcessing;

    using CarDealer.Models;

    public class PartCarImport : JsonProcess
    {
        public PartCarImport(string fullPath)
            : base(fullPath)
        {
        }
        public override void Import()
        {
            var partsCarts = JsonConvert
               .DeserializeObject<PartCar[]>(this.LoadedFile);

            this.Context.PartCars.AddRange(partsCarts);
            this.Context.SaveChanges();

            Console.WriteLine($"Successfully partsCarts imported {partsCarts.Length}");
        }
    }
}
