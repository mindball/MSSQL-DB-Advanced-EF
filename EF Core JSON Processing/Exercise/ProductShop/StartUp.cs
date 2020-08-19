namespace ProductShop
{
    using System;
    using System.IO;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using ProductShop.Data;
    using ProductShop.DTO.Category;
    using ProductShop.DTO.Product;
    using ProductShop.DTO.User;
    using ProductShop.Models;

    public class StartUp
    {       
        internal const string path = "../../../Datasets/Results/";

        public static IConfigurationProvider Config => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProductShopProfile>();
        });

        public static void Main(string[] args)
        {
            //ResetDB(new ProductShopContext());
            ProductShopContext context = new ProductShopContext();

            //string importJson = File.ReadAllText("../../../Datasets/users.json");

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

            /*
             * Query and Export Data
             */
            
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //Query: Export Products in Range
            //With Anonymous class
            //string jsonFile = GetProductsInRange(context);

            //With DTO
            //var jsonFileDto = GetProductsInRangeWithDTO(context);
            //File.WriteAllText(path + "/products-in-range.json", jsonFile);

            //Query 6.Export Successfully Sold Products
            //var jsonFileDto = GetSoldProducts(context);
            //File.WriteAllText(path + "/user-sold-item.json", jsonFileDto);

            var jsonFileDto = GetUsersWithProducts(context);
            File.WriteAllText(path + "/categories-by-products.json", jsonFileDto);

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

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = p.Seller.FirstName + " " + p.Seller.LastName
                })
                .OrderBy(p => p.price)
                .ToArray();

            var pr = Newtonsoft.Json.JsonConvert
                .SerializeObject(products, Newtonsoft.Json.Formatting.Indented);

            return pr;
        }

        public static string GetProductsInRangeWithDTO(ProductShopContext context)
        {

            var mapper = Config.CreateMapper();

            var products = context.Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .OrderBy(x => x.Price)
                .ProjectTo<ProductWithRangeDTO>(mapper.ConfigurationProvider)
                .ToList();

            var pr = Newtonsoft.Json.JsonConvert
                .SerializeObject(products, Newtonsoft.Json.Formatting.Indented);

            return pr;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var mapper = Config.CreateMapper();

            var users = context.Users
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ProjectTo<UserNameWithSoldItemsDTO>(mapper.ConfigurationProvider)
                .ToList();

            var pr = Newtonsoft.Json.JsonConvert
               .SerializeObject(users, Newtonsoft.Json.Formatting.Indented);

            return pr;
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var mapper = Config.CreateMapper();

            var users = context.Categories
                .OrderByDescending(x => x.CategoryProducts.Count)                
                .ProjectTo<GetAllCategoryWithProductCount>(mapper.ConfigurationProvider)
                .ToList();

            var pr = Newtonsoft.Json.JsonConvert
               .SerializeObject(users, Newtonsoft.Json.Formatting.Indented);


            return pr;
        }
    }
}