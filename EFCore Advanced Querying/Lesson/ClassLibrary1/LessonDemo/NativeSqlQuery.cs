using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LessonDemo
{
    using Models;

    class NativeSqlQuery
    {
        //Executing Native SQL Queries
        public static void ExecuteSqlQuery(MusicXContext mContext)
        {
            var query = "SELECT * FROM [Songs]";

            var songs = mContext.Songs
                .FromSqlRaw(query)
                .ToList();

            foreach (var song in songs)
            {
                Console.WriteLine(song.Name);
            }
        }

        //Native SQL Queries with Parameters
        public static void ExecuteSqlQueryWithParameter(MusicXContext mContext)
        {
            //SQL injection special symbols = ' OR 1=1 --
            //list all songs
            string searchString = "' OR 1=1 --";

            var unSafequery = "SELECT * FROM [Songs] WHERE [Name] Like '" + searchString + "'";
            var unSafesongs = mContext.Songs
               .FromSqlRaw(unSafequery)
               .ToList();

            //Method 1: Safe query based on (String.Format()) 
            //- test with injection special symbols = ' or 1=1 --
            var safeQueryOne = String.Format("SELECT * FROM [Songs] WHERE [Name] Like {0}", searchString);
            var safeSongs = mContext.Songs
               .FromSqlRaw(safeQueryOne)
               .ToList();

            //Method 2: with interpolation string            
            var safeSongsTwo = mContext.Songs
               .FromSqlInterpolated($"SELECT * FROM [Songs] WHERE [Name] Like {searchString}")
               .ToList();

            foreach (var song in unSafesongs)
            {
                Console.WriteLine(song.Name);
            }

            foreach (var song in safeSongs)
            {
                Console.WriteLine(song.Name);
            }
        }
    }
}
