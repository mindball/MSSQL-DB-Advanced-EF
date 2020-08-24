namespace CarDealer.Models
{
    using System;

    using Newtonsoft.Json;

    using CarDealer.Data;
    using CarDealer.Models.Contracts;

    public class SupplierImport : IImporter
    {
        public void Import(CarDealerContext context, string fileName)
        {
            var suppliers = JsonConvert
              .DeserializeObject<Supplier[]>(fileName);

            context.Suppliers.AddRange(salesuppliers;
            Console.WriteLine($"Successfully imported {suppliers.Length}");
        }
    }
}
