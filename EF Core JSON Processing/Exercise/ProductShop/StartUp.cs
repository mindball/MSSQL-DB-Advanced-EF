using Microsoft.EntityFrameworkCore;
using ProductShop.Data;
using System;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            ResetDB(new ProductShopContext());
        }

        private static void ResetDB(ProductShopContext context)
        {
            var dbName = context.Database.GetDbConnection().Database;

            context.Database.EnsureDeleted();
            Console.WriteLine($"Reset {dbName}!!!");

            context.Database.EnsureCreated();
            Console.WriteLine($"{dbName} is Up!!!");
        }
    }
}