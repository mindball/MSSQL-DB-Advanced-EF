using LessonDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LessonDemo
{
    class ConcurrencyCheck
    {
        /*  * EF Core runs in optimistic concurrency mode (no locking)
                - By default the conflict resolution strategy in EF is "last one wins"
                - The last change overwrites all previous concurrent changes

            * Enabling "first wins" strategy for certain property in EF:
                [ConcurrencyCheck] - attribute on property
        **/

        public static void ConcurrencyWithoutAttribute(MusicXContext context)
        {
            var song = context.Songs
                .FirstOrDefault(x => x.Name.Contains("Айде, aйде"));
            song.Name = "Айде, aйде (first change) ";

            var context2 = new MusicXContext();
            var song2 = context2.Songs
                .FirstOrDefault(x => x.Name.Contains("Айде, aйде"));
            song2.Name = "Айде, aйде (second change win)";

            context.SaveChanges();
            context2.SaveChanges();
        }

        public static void ConcurrencyWithAttribute(MusicXContext context)
        {            
            var song = context.Songs.FirstOrDefault(x => x.Name.Contains("Айде, aйде"));
            song.Name = "Айде, aйде (first change win)";

            var context2 = new MusicXContext();
            var song2 = context2.Songs
                    .FirstOrDefault(x => x.Name.Contains("Айде, aйде%"));
            song2.Name = "Айде, aйде (second change loose)";

            context.SaveChanges();

            try
            {
                context2.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                Console.WriteLine("First context win");                
            }
        }

        public static void ConcurrencyWithAttributeDifferentLogic(MusicXContext context)
        {
            //При този вариант може да вкараме и цикъл докато не променим property-to
            var song = context.Songs.FirstOrDefault(x => x.Name.Contains("Айде, aйде"));
            song.Name = "Айде, aйде 'change first'";

            var context2 = new MusicXContext();
            var song2 = context2.Songs
                    .FirstOrDefault(x => x.Name.Contains("Айде, aйде"));
            song2.Name = "Айде, aйде 'second change'";

            context.SaveChanges();

            try
            {
                context2.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {                
                var context3 = new MusicXContext();
                var song3 = context3.Songs
                        .FirstOrDefault(x => x.Name.Contains("Айде, aйде"));
                song3.Name = "Айде, aйде (When we shure to change this entry!!!!)";

                context3.SaveChanges();
            }
        }

    }
}
