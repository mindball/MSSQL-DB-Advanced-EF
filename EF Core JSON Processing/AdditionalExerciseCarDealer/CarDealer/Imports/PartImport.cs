namespace CarDealer.Imports
{
    using System;
    using Newtonsoft.Json;

    using CarDealer.Data;
    using CarDealer.Imports.FileProcessing;

    using CarDealer.Models;

    public class PartImport : JsonProcess
    {
        public PartImport(string fullPath)
            : base(fullPath)
        {

        }
        public override void Import()
        {
            var parts = JsonConvert
              .DeserializeObject<Part[]>(this.LoadedFile);

            this.Context.Parts.AddRange(parts);
            this.Context.SaveChanges();

            Console.WriteLine($"Successfully parts imported {parts.Length}");
        }
    }
}
