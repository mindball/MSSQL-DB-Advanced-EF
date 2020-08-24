using System;

using CarDealer.Contracts;
using CarDealer.Imports;

namespace CarDealer.Factories
{
    public class ImportCarEntityFactory
    {      
        public IJsonProcess GetImport(string fileName, string fullPath)
        {
            IJsonProcess importer;

            if (fileName.ToLower().Equals("1cars.json"))
            {
                importer = new CarImport(fullPath);
            }
            else if(fileName.ToLower().Equals("2customers.json"))
            {
                importer = new CustomerImport(fullPath);
            }
            else if (fileName.ToLower().Equals("5parts.json"))
            {
                importer = new PartImport(fullPath);
            }
            else if (fileName.ToLower().Equals("3sales.json"))
            {
                importer = new SaleImport(fullPath);
            }
            else if (fileName.ToLower().Equals("4suppliers.json"))
            {
                importer = new SupplierImport(fullPath);
            }       
            else
            {
                throw new ArgumentException("No such importes");
            }

            return importer;
        }
    }
}
