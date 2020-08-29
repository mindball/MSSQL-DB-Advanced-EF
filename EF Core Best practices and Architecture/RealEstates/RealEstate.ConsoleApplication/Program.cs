using RealEstate.Data;
using System;

namespace RealEstate.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new RealEstateDbContext();

            db.Database.EnsureDeleted();
        }
    }
}
