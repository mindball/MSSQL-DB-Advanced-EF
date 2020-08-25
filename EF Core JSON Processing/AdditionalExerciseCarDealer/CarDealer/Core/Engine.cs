namespace CarDealer.Core
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using CarDealer.Contracts;
    using CarDealer.Data;
    using CarDealer.Factories;    
    using CarDealer.DTO;
    using Newtonsoft.Json;
    using CarDealer.Exports;

    class Engine
    {


        private ImportCarEntityFactory importer;
        private CarDealerContext context;
        private IExporter jsonExporter;

        public Engine()
        {
            this.importer = new ImportCarEntityFactory();
            this.context = new CarDealerContext();

            this.jsonExporter = new JsonExporter();
        }

        private IConfigurationProvider Config => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CarDealerProfile>();
        });

        public IMapper Mapper => this.Config.CreateMapper();

        public void Run()
        {
            string path = "../../../Exports/JsonDataSets/";

            //ImportProcess(path);

            ExportProcess(path);            
        }

        private void ExportProcess(string path)
        {
            //Query 13. Export Ordered Customers
            //GetOrderedCustomers(path);

            //Export Cars from Make Toyota
            GetCarsFromMakeToyota(path);
        }

        private void ImportProcess(string path)
        {
            var getFiles = GetDirectoryFiles(path);

            foreach (var fileName in getFiles)
            {
                IJsonProcess entity = importer.GetImport(Path.GetFileName(fileName), fileName);

                entity.Import();
            }
        }

        public void GetOrderedCustomers(string path)
        {
            var customers = this.context.Customers
                .OrderBy(x => x.BirthDate)
                .ThenBy(x => x.IsYoungDriver)
                .ProjectTo<OrderedCustomer>(this.Mapper.ConfigurationProvider)
                .ToList();

            string customerResult = JsonConvert
              .SerializeObject(customers, new JsonSerializerSettings()
              { 
                  NullValueHandling = NullValueHandling.Ignore,
                  DateFormatString = "dd/MM/yyyy",
                  Formatting = Formatting.Indented
              });

            string fileName = "ordered-customers.json";

            string fullDirectoryPathWithFileName =
                DirectoryExist(path, fileName);

            WriteFile(fullDirectoryPathWithFileName, customerResult);
            //Polymorph
            //this.jsonExporter.Export(fullDirectoryPathWithFileName, customerResult);
        }

        public void GetCarsFromMakeToyota(string path)
        {
            string fileName = "toyota-cars.json";

            var carsModelToyota = this.context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenBy(c => c.TravelledDistance)
                .ProjectTo<CarFromMakeToyota>(this.Mapper.ConfigurationProvider)
                .ToList();

            string fullDirectoryPathWithFileName =
                DirectoryExist(path, fileName);

            string carResult = JsonConvert
              .SerializeObject(carsModelToyota, new JsonSerializerSettings()
              {
                  NullValueHandling = NullValueHandling.Ignore,                 
                  Formatting = Formatting.Indented
              });

            WriteFile(fullDirectoryPathWithFileName, carResult);
        }

        private string DirectoryExist(string path, string fileName)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path + fileName;
        }

        private void WriteFile(string fullDirectoryPathWithFileName,
            string fileContent)
        {
            File.WriteAllText(fullDirectoryPathWithFileName, fileContent);
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

        private void ResetDB()
        {
            var dbName = this.context.Database.GetDbConnection().Database;

            context.Database.EnsureDeleted();
            Console.WriteLine($"Reset {dbName}!!!");

            context.Database.EnsureCreated();
            Console.WriteLine($"{dbName} is Up!!!");
        }
    }
}
