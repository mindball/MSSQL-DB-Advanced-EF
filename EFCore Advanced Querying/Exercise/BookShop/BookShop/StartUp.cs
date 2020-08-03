namespace BookShop
{
    using System;
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
                var results = GetBooksNotReleasedIn(dbContext);

                ////6.Book Titles by Category
                //string input = Console.ReadLine();
                //var results = GetBooksByCategory(dbContext, input);


                foreach (var result in results)
                {
                    Console.Write(result);
                }
            }
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
