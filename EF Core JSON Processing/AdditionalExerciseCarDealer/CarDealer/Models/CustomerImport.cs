namespace CarDealer.Models
{
    using System;

    using Newtonsoft.Json;

    using CarDealer.Data;
    using CarDealer.Models.Contracts;
    public class CustomerImport : IImporter
    {
        public void Import(CarDealerContext context, string fileName)
        {
            var customers = JsonConvert
               .DeserializeObject<Customer[]>(fileName);

            context.Customers.AddRange(customers);
            Console.WriteLine($"Successfully imported {customers.Length}");
        }
    }
}
