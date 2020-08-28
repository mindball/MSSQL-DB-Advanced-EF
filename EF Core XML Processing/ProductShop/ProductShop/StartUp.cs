namespace ProductShop
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models;
    using ProductShop.DTO.Export;
    using ProductShop.Dtos.Import;

    public class StartUp
    {
        internal const string path = "../../../Datasets/Results/";        

        public static IConfigurationProvider Config => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProductShopProfile>();
        });

        public static IMapper mapper => Config.CreateMapper();

        public static void Main(string[] args)
        {
            using var context = new ProductShopContext();

            //var userXml = File.ReadAllText(@"./../../../Datasets/users.xml");
            //var usersResult = ImportUsers(context, userXml);
            //var usersResult = ImportUsersWithDTO(context, userXml);

            //var productXml = File.ReadAllText(@"./../../../Datasets/products.xml");
            //var productResult = ImportProducts(context, productXml);
            //var productResult = ImportProductsWithDTO(context, productXml);

            //var categoriesXml = File.ReadAllText(@"./../../../Datasets/categories.xml");
            //var categoriesResult = ImportCategories(context, categoriesXml);
            //var categoriesResult = ImportCategoriesDTO(context, categoriesXml)

            //var categoriesProductsXml = File.ReadAllText(@"./../../../Datasets/categories-products.xml");
            //var categoriesProductsResult = ImportCategoryProducts(context, categoriesProductsXml);
            //var categoriesProductsResult = ImportCategoryProductsWithDTO(context, categoriesProductsXml);

            var result = GetProductsInRange(context);
            File.WriteAllText("./../../../Datasets/Export/products-in-range.xml", result);
            Console.WriteLine(result);

        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {            
            XDocument doc = XDocument.Load(inputXml);

            var usersFromXml = doc.Root
                .Elements()
                .ToList();

            var users = new List<User>();

            usersFromXml.ForEach(x =>
                {
                    User currentUser = new User();

                    currentUser.FirstName = x.Element("firstName").Value;
                    currentUser.LastName = x.Element("lastName").Value;
                    currentUser.Age = int.Parse(x.Element("age").Value);

                    users.Add(currentUser);
                });

            context.AddRange(users);

            int affectedRows = context.SaveChanges();

            return $"Successfully imported {affectedRows}";
        }

        public static string ImportUsersWithDTO(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportUserDto[]), new XmlRootAttribute("Users"));

            using var reader = new StringReader(inputXml);

            var usersDto = (ImportUserDto[])xmlSerializer.Deserialize(reader);

            var users = new List<User>();

            foreach (var userDto in usersDto)
            {
                var user = mapper.Map<User>(userDto);

                users.Add(user);
            }

            int affectedRows = context.SaveChanges();

            return $"Successfully imported {affectedRows}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            XDocument doc = XDocument.Parse(inputXml);

            var categoriesFromXml = doc.Root
                .Elements()
                .ToList();

            var categories = new List<Category>();

            categoriesFromXml.ForEach(x =>
            {
                Category currentCategory = new Category();

                currentCategory.Name = x.Element("name").Value;
                categories.Add(currentCategory);
            });

            context.Categories.AddRange(categories);
            int affectedRows = context.SaveChanges();
            return $"Successfully imported {affectedRows}";
        }

        public static string ImportCategoriesDTO(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCategoryDto[]), new XmlRootAttribute("Categories"));

            var categoriesDto = (ImportCategoryDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            var categories = new List<Category>();

            foreach (var categoryDto in categoriesDto)
            {
                if (categoryDto.Name == null)
                {
                    continue;
                }

                var category = mapper.Map<Category>(categoryDto);
                categories.Add(category);
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            XDocument doc = XDocument.Parse(inputXml);

            var categoryProductsFromXml = doc.Root
                .Elements()
                .ToList();

            List<CategoryProduct> pairs = new List<CategoryProduct>();

            foreach (var cp in categoryProductsFromXml)
            {
                CategoryProduct currentPair = new CategoryProduct();

                var categoryId = int.Parse(cp.Element("CategoryId").Value);
                var productId = int.Parse(cp.Element("ProductId").Value);

                if (categoryId == 0 || productId == 0)
                    continue;

                currentPair.CategoryId = categoryId;
                currentPair.ProductId = productId;
                pairs.Add(currentPair);
            };

            pairs = pairs.Distinct().ToList();
            context.CategoryProducts.AddRange(pairs);

            int affectedRows = context.SaveChanges();
            return $"Successfully imported {affectedRows}";
        }

        public static string ImportCategoryProductsWithDTO
            (ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCategoryProductDto[]), new XmlRootAttribute("CategoryProducts"));

            var categoriesProductsDto = (ImportCategoryProductDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            var categoriesProducts = new List<CategoryProduct>();

            foreach (var categoryProductDto in categoriesProductsDto)
            {
                var product = context.Products.Find(categoryProductDto.ProductId);
                var category = context.Categories.Find(categoryProductDto.CategoryId);

                if (product == null || category == null)
                {
                    continue;
                }

                var categoryProduct = mapper.Map<CategoryProduct>(categoryProductDto);               

                categoriesProducts.Add(categoryProduct);
            }

            context.CategoryProducts.AddRange(categoriesProducts);
            context.SaveChanges();

            return $"Successfully imported {categoriesProducts.Count}";
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            XDocument doc = XDocument.Parse(inputXml);

            var productsFromXml = doc.Root
                .Elements()
                .ToList();

            var products = new List<Product>();

            productsFromXml.ForEach(x =>
            {
                Product currentProduct = new Product();
                currentProduct.Name = x.Element("name").Value;
                currentProduct.Price = decimal.Parse(x.Element("price").Value);

                var sellerId = int.Parse(x.Element("sellerId").Value);
                var buyerId = int.Parse(x.Element("sellerId").Value);

                currentProduct.SellerId = sellerId;
                currentProduct.BuyerId = buyerId == 0 ? null : (int?)buyerId;

                products.Add(currentProduct);
            });

            context.Products.AddRange(products);

            int affectedRows = context.SaveChanges();
            return $"Successfully imported {affectedRows}";
        }

        public static string ImportProductsWithDTO(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportProductDto[]), new XmlRootAttribute("Products"));

            var productsDto = (ImportProductDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            var products = new List<Product>();

            foreach (var productDto in productsDto)
            {
                var product = Mapper.Map<Product>(productDto);
                products.Add(product);
            }

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .ProjectTo<ProductInRangeDto>(mapper.ConfigurationProvider)
                .ToList()
                .Take(10)
                .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProductInRangeDto[]), new XmlRootAttribute("Products"));

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            xmlSerializer.Serialize(new StringWriter(sb), products, namespaces);

            return sb.ToString().TrimEnd();
        }
    }
}