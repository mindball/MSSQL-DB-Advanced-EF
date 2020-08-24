namespace CarDealer.Imports
{
    using System;
    using Newtonsoft.Json;

    using CarDealer.Data;
    using CarDealer.Imports.FileProcessing;

    using CarDealer.Models;

    public class SaleImport : JsonProcess
    {
        public SaleImport(string fullPath)
            : base(fullPath)
        {
        }

        public override void Import()
        {
            var sales = JsonConvert
              .DeserializeObject<Sale[]>(this.LoadedFile);

            this.Context.Sales.AddRange(sales);
            this.Context.SaveChanges();

            Console.WriteLine($"Successfully sales imported {sales.Length}");
        }
    }
}
