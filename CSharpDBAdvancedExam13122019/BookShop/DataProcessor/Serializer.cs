namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using BookShop.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var authors = context.Authors
                .Select(a => new
                {
                    AuthorName = a.FirstName + " " + a.LastName,
                    Books = a.AuthorsBooks
                             .OrderByDescending(b => b.Book.Price)
                             .Select(b => new
                             {
                                 BookName = b.Book.Name,
                                 BookPrice = b.Book.Price.ToString("f2")
                             })
                             //When convert decimal to string, orderring has matter
                             //.OrderByDescending(b => b.BookPrice)
                             .ToList()
                })
                .OrderByDescending(a => a.Books.Count)
                .ThenBy(a => a.AuthorName)
                .ToList();  

            var outputJson = JsonConvert.SerializeObject(authors, Formatting.Indented);

            return outputJson;
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            var books = context.Books
                 .Where(b => b.PublishedOn < date
                                && b.Genre.ToString() == "Science")
                 .Select(b => new ExportBookDto
                 {
                     BookPage = b.Pages,
                     Name = b.Name,
                     DateTime = b.PublishedOn.ToString("d", CultureInfo.InvariantCulture)
                 })
                 .OrderByDescending(b => b.BookPage)
                 .ThenBy(b => b.DateTime)
                 .Take(10)
                 .ToList();

            var xmlSerializer = new XmlSerializer(typeof(List<ExportBookDto>),
                                             new XmlRootAttribute("Books"));

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            using (var writer = new StringWriter(sb))
            {
                xmlSerializer.Serialize(writer, books, namespaces);
            }

            return sb.ToString().TrimEnd();

        }
    }
}