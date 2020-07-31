using LessonDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LessonDemo
{
    class TypesOfLoading
    {
        //Добра практиа е да се използва проекция !!!! (select)

        public static void LazyType(MusicXContext context)
        {
            //Enable lazy loading:
            //Install-Package Microsoft.EntityFrameworkCore.Proxies
            //(DbContextOptionsBuilder options) -> 
            //option..UseLazyLoadingProxies().UseSqlServer(myConnectionString);


        }

        public static void EagerType(MusicXContext context)
        {
            //1. По често се използва от Explicit
            //2. Eager loading loads all related records of an entity at once
            
            var song = context.Songs
                .Include(x => x.Source)
                .Include(x => x.SongMetadata)
                .Where(x => x.Name.StartsWith("Осъдени души"))
                .FirstOrDefault();
        }

        public static void ExplicitType(MusicXContext context)
        {
            //1. Explicit loading loads all records when they’re needed
            //2. Performed with the Collection().Load() method
            //3. Важи само за едно пропърти song (един обект)

            //Examle when is ordinary property
            var song = context.Songs
                .Where(x => x.Name.StartsWith("Осъдени души"))
                .FirstOrDefault();

            context.Entry(song)
                .Reference(x => x.Source)
                .Load();

            //Example when is collection property
            context.Entry(song)
                .Collection(x => x.SongMetadata)
                .Load();

            Console.WriteLine();

        }


    }
}
