using Microsoft.EntityFrameworkCore;
using ProductShop.Data;
using ProductShop.Models;
using System;
using System.IO;
using System.Linq;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            //ResetDB(new ProductShopContext());
            ProductShopContext context = new ProductShopContext();

            string importJson = File.ReadAllText("../../../Datasets/users.json");

            //Query 2. Import Users
            //Console.WriteLine(ImportUsers(context, importJson));

            ////Query 3.Import Products
            //importJson = File.ReadAllText("../../../Datasets/products.json");
            //Console.WriteLine(ImportProducts(context, importJson));

            //////Query 4. Import Categories
            //importJson = File.ReadAllText("../../../Datasets/categories.json");
            //Console.WriteLine(ImportCategories(context, importJson));

            ////Query 5. Import Categories and Products
            //importJson = File.ReadAllText("../../../Datasets/categories-products.json");
            //Console.WriteLine(ImportCategoryProducts(context, importJson));

        }

        private static void ResetDB(ProductShopContext context)
        {
            var dbName = context.Database.GetDbConnection().Database;

            context.Database.EnsureDeleted();
            Console.WriteLine($"Reset {dbName}!!!");

            context.Database.EnsureCreated();
            Console.WriteLine($"{dbName} is Up!!!");
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            //use microsoft library
            var usersM = System.Text.Json.JsonSerializer
                .Deserialize<User[]>(inputJson);

            //use Newtons
            var userN = Newtonsoft.Json.JsonConvert
                .DeserializeObject<User[]>(inputJson);

            context.Users.AddRange(userN);
            int rowAffected = context.SaveChanges();

            var output = $"Successfully imported {usersM.Length}";

            return output;
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = Newtonsoft.Json.JsonConvert
                .DeserializeObject<Product[]>(inputJson);

            context.Products.AddRange(products);
            int rowAffected = context.SaveChanges();

            var output = $"Successfully imported {products.Length}";
            return output;
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            //var jsonSettings = new Newtonsoft.Json.JsonSerializerSettings()
            //{
            //    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            //};

            //var categories = Newtonsoft.Json.JsonConvert
            //    .DeserializeObject<Category[]>(inputJson, jsonSettings);             

            var categories = Newtonsoft.Json.JsonConvert
                .DeserializeObject<Category[]>(inputJson)
                .Where(c => c.Name != null)
                .ToArray();

            context.Categories.AddRange(categories);
            int rowAffected = context.SaveChanges();

            var output = $"Successfully imported {categories.Length}";
            return output;
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = Newtonsoft.Json.JsonConvert
                .DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoryProducts.AddRange(categoryProducts);
            int rowAffected = context.SaveChanges();

            var output = $"Successfully imported {categoryProducts.Length}";
            return output;
        }
    }
}