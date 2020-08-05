namespace BookShop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Data;
    using Initializer;

    //The LINQ expression 'DbSet<Book>
    //' could not be translated. Either rewrite the query in a form that can be translated, 
    //or switch to client evaluation explicitly by inserting a call to either
    //AsEnumerable(), AsAsyncEnumerable(), ToList(), or ToListAsync()

    public class StartUp
    {
        static void Main(string[] args)
        {
                   

            using (var dbContext = new BookShopContext())
            {
                //DbInitializer.ResetDatabase(db);
                
                //2. Age Restriction
                var input = Console.ReadLine().ToLower();
                var results = GetBooksByAgeRestriction(dbContext, input);

                //3. Golden Books
                results = GetGoldenBooks(dbContext);

                //4.Books by Price
                results = GetBooksByPrice(dbContext);

                //5.Not Released In
                results = GetBooksNotReleasedIn(dbContext);

                //////6.Book Titles by Category
                input = Console.ReadLine();
                results = GetBooksByCategory(dbContext, input);

                //7. Released Before Date
                string date = Console.ReadLine();
                results = GetBooksReleasedBefore(dbContext, date);

                ////8.Author Search
                string str = Console.ReadLine();
                results = GetAuthorNamesEndingIn(dbContext, str);

                //9.Book Search
                str = Console.ReadLine();
                results = GetBookTitlesContaining(dbContext, str);

                //10. Book Search by Author
                str = Console.ReadLine();
                results = GetBooksByAuthor(dbContext, date);

                //11. Count Books
                int len = int.Parse(Console.ReadLine());
                Console.WriteLine(CountBooks(dbContext, len));

                //12.Total Book Copies
                results = CountCopiesByAuthor(dbContext);

                //Mapped table joins
                //13. Profit by Category
                results = GetTotalProfitByCategory(dbContext);

                //Mapped table joins
                //14. Most Recent Books
                results = GetMostRecentBooks(dbContext);

                foreach (var result in results)
                {
                    Console.Write(result);
                }

                //15. Increase Prices
                //All records in DB are 190
                Console.WriteLine(IncreasePrices(dbContext));

                //16.Remove Books
                RemoveBooks(dbContext);

            }
        }

        public static int RemoveBooks(BookShopContext context, int lessThanCopies = 4200)
        {
            var books = context.Books
                    .Where(b => b.Copies < lessThanCopies)
                    .ToArray();

            var removedBooks = books.Length;

            context.Books.RemoveRange(books);
            context.SaveChanges();

            return removedBooks;
        }

        public static int IncreasePrices(BookShopContext context,
            decimal increasement = 5, int year = 2010)
        {     
            using (context)
            {
                var books = context.Books
                     .Where(b => b.ReleaseDate.Value.Year < year)
                     .ToList();

                foreach (var book in books)
                {
                    book.Price += increasement;
                }

                return context.SaveChanges();
            }
        }

        //Mapped table joins
        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder str = new StringBuilder();

            var mostRecentBooks = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    BooksName = c.BooksCategories
                                .Select(b => new
                                {
                                    Title = b.Book.Title,
                                    Year = b.Book.ReleaseDate.Value.Year
                                })
                                .OrderByDescending(y => y.Year)
                                .Take(3)
                                .ToList()
                }
                    )
                .OrderBy(c => c.CategoryName)
                .ToList();

            foreach (var category in mostRecentBooks)
            {
                str.AppendLine($"--{category.CategoryName}");
                foreach (var book in category.BooksName)
                {
                    str.AppendLine($"{book.Title} ({book.Year})");
                }
            }

            return str.ToString().TrimEnd();
        }

        //Mapped table joins
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder str = new StringBuilder();

            var totalProfit = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    ProfitOfBooks = c.BooksCategories
                                        .Select(b => new
                                        {
                                            BookProfit = b.Book.Price * b.Book.Copies
                                        })
                                        .Sum(d => d.BookProfit)
                })
                .OrderByDescending(c => c.ProfitOfBooks)
                .ThenBy(c => c.CategoryName)
                .ToList();

            foreach (var item in totalProfit)
            {
                str.AppendLine($"{item.CategoryName} ${item.ProfitOfBooks:f2}");
            }

            return str.ToString().TrimEnd();
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder str = new StringBuilder();
            var results = context.Authors
                .Select(b => new
                {
                    AuthorFullName = $"{b.FirstName} {b.LastName}",
                    SumOfCopies = b.Books.Sum(br => br.Copies)
                })
                .OrderByDescending(a => a.SumOfCopies)
                .ToList(); ;

            foreach (var ath in results)
            {
                str.AppendLine($"{ath.AuthorFullName} - {ath.SumOfCopies }");
            }

            return string.Join(Environment.NewLine, str);
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var len = context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();

            return len;
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var booksByGivenAuthor = context.Books
                .Where(a => a.Author
                            .LastName
                            .ToLower()
                            .StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(ba => $"{ba.Title} ({ba.Author.FirstName} {ba.Author.LastName})");

            return string.Join(Environment.NewLine, booksByGivenAuthor);
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(t => t.Title)
                .OrderBy(t => t);

            return string.Join(Environment.NewLine, books);
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(fullname => fullname.FirstName 
                                + " " + fullname.LastName)
                .OrderBy(a => a)
                .ToList();

            return string.Join(Environment.NewLine, authors); ;
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime dateTime = DateTime.Parse(date);

            var titles = context.Books.
                Where(b => b.ReleaseDate < dateTime)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(t => t.Title + 
                        " - " + 
                        t.EditionType 
                        + " - "                         
                        + t.Price
                )
                .ToList();

            return string.Join(Environment.NewLine, titles);
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] args = input.Split();
            List<string> groups = new List<string>();

            foreach (var arg in args)
            {
                var filter = context.Books
                    .Where(b => b.BooksCategories.Any(x => x.Category.Name.ToLower().Equals(arg)))
                    .Select(b => b.Title).ToList();

                groups.AddRange(filter);
            }

            groups = groups.OrderBy(b => b).ToList();

            return string.Join(Environment.NewLine,
                 groups);
        }

        private static string GetBooksNotReleasedIn(BookShopContext dbContext)
        {
            string result = "";

            string releaseDate = Console.ReadLine();            

            result = string.Join(Environment.NewLine,
                 dbContext.Books
                .Where(t => t.ReleaseDate.Value.Year.ToString() != releaseDate)
                .OrderBy(p => p.BookId)
                .Select(b => b.Title)
                .ToList());

            return result;
        }

        private static string GetBooksByPrice(BookShopContext dbContext)
        {
            string result = "";
            decimal value = 40;

            result = string.Join(Environment.NewLine,
                dbContext.Books
                .Where(t => t.Price > value)
                .OrderByDescending(p => p.Price)
                .Select(b => $"{b.Title} - ${b.Price:F2}"));               

            return result;
        }

        public static string GetBooksByAgeRestriction(BookShopContext context,
            string command)
        {
            string result = "";

            
            var titlies = context.Books 
                .ToList();

            result = string.Join(Environment.NewLine,
                titlies.Where(b => b.AgeRestriction.ToString()
                            .Equals(command, StringComparison.OrdinalIgnoreCase))
                .Select(b => b.Title)
                .OrderBy(b => b));

            return result;
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            string result = "";
            var titlies = context.Books
                .OrderBy(b => b.BookId)
               .ToList();

            result = string.Join(Environment.NewLine,
                 titlies.Where(t => t.EditionType.ToString().Equals("Gold") 
                 && t.Copies < 5000)                 
                 .Select(b => b.Title)                 
                 );

            return result;
        }
    }
}
