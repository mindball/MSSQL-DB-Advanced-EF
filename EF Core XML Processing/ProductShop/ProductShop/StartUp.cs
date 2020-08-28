namespace ProductShop
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using AutoMapper;

    using Data;
    using Models;
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

            var userXml = File.ReadAllText(@"./../../../Datasets/users.xml");

            var usersResult = ImportUsersWithDTO(context, userXml);
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            //
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
    }
}