using LessonDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;

namespace LessonDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            var mContext = new MusicXContext();

            //Executing Native SQL Queries
            ExecuteNativeSqlQuery(mContext);

            //Native SQL Queries with Parameters
            ExecuteNativeSqlQueryWithParameter(mContext);

        }

        //Executing Native SQL Queries
        public static void ExecuteNativeSqlQuery(MusicXContext mContext)
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

        public static void ExecuteNativeSqlQueryWithParameter(MusicXContext mContext)
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
