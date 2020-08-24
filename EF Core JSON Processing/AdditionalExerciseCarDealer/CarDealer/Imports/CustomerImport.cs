namespace CarDealer.Imports
{
    using System;
    using Newtonsoft.Json;
    
    using CarDealer.Imports.FileProcessing;

    using CarDealer.Models;

    public class CustomerImport : JsonProcess
    {

        public CustomerImport(string fullPath)
            : base(fullPath)
        {
        }

        public override void Import()
        {
            var customers = JsonConvert
               .DeserializeObject<Customer[]>(this.LoadedFile);

            this.Context.Customers.AddRange(customers);
            this.Context.SaveChanges();

            Console.WriteLine($"Successfully customers imported {customers.Length}");
        }
    }
}
