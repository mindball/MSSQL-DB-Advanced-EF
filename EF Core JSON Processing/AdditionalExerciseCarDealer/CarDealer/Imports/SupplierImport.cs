namespace CarDealer.Imports
{
    using System;
    using Newtonsoft.Json;

    using CarDealer.Data;
    using CarDealer.Imports.FileProcessing;

    using CarDealer.Models;

    public class SupplierImport : JsonProcess
    {
        public SupplierImport(string fullPath)
            : base(fullPath)
        {
        }

        public override void Import()
        {
            var suppliers = JsonConvert
              .DeserializeObject<Supplier[]>(this.LoadedFile);

            this.Context.Suppliers.AddRange(suppliers);
            this.Context.SaveChanges();          

            Console.WriteLine($"Successfully suppliers imported {suppliers.Length}");
        }
    }
}
