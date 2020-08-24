using CarDealer.Data;
using CarDealer.Factories;
using CarDealer.Models.Contracts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CarDealer.Core
{
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
                IImporter entity = importer.GetImport(Path.GetFileName(fileName));
            }

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

        private static void ResetDB(CarDealerContext context)
        {
            var dbName = context.Database.GetDbConnection().Database;

            context.Database.EnsureDeleted();
            Console.WriteLine($"Reset {dbName}!!!");

            context.Database.EnsureCreated();
            Console.WriteLine($"{dbName} is Up!!!");
        }


    }
}
