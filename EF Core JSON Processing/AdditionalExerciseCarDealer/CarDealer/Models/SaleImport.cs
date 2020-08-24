namespace CarDealer.Models
{
    using System;

    using Newtonsoft.Json;

    using CarDealer.Data;
    using CarDealer.Models.Contracts;

    public class SaleImport : IImporter
    {
        public void Import(CarDealerContext context, string fileName)
        {
            var sale = JsonConvert
              .DeserializeObject<Sale[]>(fileName);

            context.Sales.AddRange(sale);
            Console.WriteLine($"Successfully imported {sale.Length}");
        }
    }
}
