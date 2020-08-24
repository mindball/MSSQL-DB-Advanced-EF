using CarDealer.Models;
using CarDealer.Models.Contracts;
using System;

namespace CarDealer.Factories
{
    public class ImportCarEntityFactory
    {      
        public IImporter GetImport(string fileName)
        {
            IImporter importer;

            if (fileName.ToLower().Equals("cars.json"))
            {
                importer = new CarImport();
            }
            else if(fileName.ToLower().Equals("customers.json"))
            {
                importer = new CustomerImport();
            }
            else if (fileName.ToLower().Equals("parts.json"))
            {
                importer = new PartImport();
            }
            else if (fileName.ToLower().Equals("sales.json"))
            {
                importer = new SaleImport();
            }
            else if (fileName.ToLower().Equals("suppliers.json"))
            {
                importer = new SupplierImport();
            }       
            else
            {
                throw new ArgumentException("No such importes");
            }

            return importer;
        }
    }
}
