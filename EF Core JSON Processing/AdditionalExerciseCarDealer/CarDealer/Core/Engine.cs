using CarDealer.Contracts;
using CarDealer.Data;
using CarDealer.Factories;


namespace CarDealer.Core
{
    using System;
    using System.IO;
    using Microsoft.EntityFrameworkCore;

    using CarDealer.Contracts;
    using CarDealer.Data;
    using CarDealer.Factories;
    using CarDealer.Imports;

    class Engine
    {
        private ImportCarEntityFactory importer;
        private CarDealerContext context;

        public Engine()
        {
            this.importer = new ImportCarEntityFactory();
            this.context = new CarDealerContext();
        }

        public void Run()
        {
            string path = "../../../Datasets/";

            var getFiles = GetDirectoryFiles(path);

            foreach (var fileName in getFiles)
            {
                IJsonProcess entity = importer.GetImport(Path.GetFileName(fileName), fileName);

                entity.Import();
            }

            ////1.Successfully
            //CarImport car = new CarImport("../../../Datasets/cars.json");
            //car.Import();

            ////2.Successfully
            //CustomerImport customer = new CustomerImport("../../../Datasets/customers.json");
            //customer.Import();

            ////3.Successfully
            //SaleImport sale = new SaleImport("../../../Datasets/sales.json");
            //sale.Import();

            ////4.Successfully
            //SupplierImport supplier = new SupplierImport("../../../Datasets/suppliers.json");
            //supplier.Import();

            ////5.Successfully
            //PartImport part = new PartImport("../../../Datasets/parts.json");
            //part.Import();

        }

        private string[] GetDirectoryFiles(string path)
        {            
            try
            {
                string[] fileEntries = Directory.GetFiles(path);
                return fileEntries;
            }
            catch(DirectoryNotFoundException dirEx)
            {
                throw new Exception(dirEx.Message);
            }
        }

        public void ResetDB()
        {
            var dbName = this.context.Database.GetDbConnection().Database;

            context.Database.EnsureDeleted();
            Console.WriteLine($"Reset {dbName}!!!");

            context.Database.EnsureCreated();
            Console.WriteLine($"{dbName} is Up!!!");
        }


    }
}
