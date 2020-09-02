using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Services;
using System;
using System.Linq;

namespace RealEstate.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var db = new RealEstateDbContext();
            //db.Database.EnsureDeleted();
            db.Database.Migrate();

            IPropertiesService propertyService = new PropertiesService(db);
            var filteredProperties = propertyService.SearchByPrice(0, 20000);

            var squareMetterSymbol = "m\u00b2";
            foreach (var property in filteredProperties)
            {
                Console.WriteLine($"{property.District}, " +
                    $"{property.Floor}, " +
                    $"{property.Size} {squareMetterSymbol}, " +
                    $"{property.Year}, " +
                    $"{property.Price} €, " +
                    $"{property.PropertyType}, " +
                    $"{property.BuildingType}");
            }

            IDistrictsService districtService = new DistrictsService(db);
            var districts = districtService.GetTopDistrictByAveragePrice(100);

            foreach (var district in districts)
            {
                Console.WriteLine($"{district.Name} => " +
                    $"Price: {district.AvgPrice:f2} " + 
                    $"({district.MinPrice} - {district.MaxPrice}) " + 
                    $"{district.PropertiesCount} properties");
            }
        }
    }
}
