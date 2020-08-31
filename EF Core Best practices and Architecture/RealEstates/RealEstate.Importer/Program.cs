using RealEstate.Data;
using RealEstate.Services;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace RealEstate.Importer
{
    class Program
    {
        static void Main()
        {
            var readJson = File.ReadAllText("imot.bg.json");

            var jsonProperies = JsonSerializer.Deserialize<JsonPropery[]>(readJson);

            var dbContext = new RealEstateDbContext();

            IPropertiesService propertiesService = new PropertiesService(dbContext);

            foreach (var property in jsonProperies.Where(x => x.Price > 1000))
            {
                propertiesService.Create(
                    property.Size,
                    property.Floor,
                    property.TotalFloors,
                    property.District,
                    property.Type,
                    property.BuildingType,
                    property.Year,
                    property.Price);
            }
        }
            
    }
}
