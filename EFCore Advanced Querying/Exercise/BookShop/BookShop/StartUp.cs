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

    class StartUp
    {
        static void Main(string[] args)
        {
                   

            using (var dbContext = new BookShopContext())
            {
                //DbInitializer.ResetDatabase(db);

                //2. Age Restriction
                //var input = Console.ReadLine().ToLower();
                //var results = GetBooksByAgeRestriction(dbContext, input);

                //3. Golden Books
                //var results = GetGoldenBooks(dbContext);

                //4.Books by Price
                //var results = GetBooksByPrice(dbContext);

                //5.Not Released In
                //var results = GetBooksNotReleasedIn(dbContext);

                //////6.Book Titles by Category
                //string input = Console.ReadLine();
                //var results = GetBooksByCategory(dbContext, input);

                //7. Released Before Date
                //string date = Console.ReadLine();
                //var results = GetBooksReleasedBefore(dbContext, date);

                ////8.Author Search
                //string str = Console.ReadLine();
                //var results = GetAuthorNamesEndingIn(dbContext, date);

                //9.Book Search
                //string str = Console.ReadLine();
                //var results = GetBookTitlesContaining(dbContext, date);

                //10. Book Search by Author
                //string str = Console.ReadLine();
                //var results = GetBooksByAuthor(dbContext, date);

                //11. Count Books
                //int len = int.Parse(Console.ReadLine());
                //Console.WriteLine(CountBooks(dbContext, len));

                //12.Total Book Copies
                var results = CountCopiesByAuthor(dbContext);

                foreach (var result in results)
                {
                    Console.Write(result);
                }
            }
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var results = context.Authors
                .Select(b => $"{b.FirstName} {b.LastName} - {b.Books.Sum(br => br.Copies)}");
                
            return string.Join(Environment.NewLine, results);
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
