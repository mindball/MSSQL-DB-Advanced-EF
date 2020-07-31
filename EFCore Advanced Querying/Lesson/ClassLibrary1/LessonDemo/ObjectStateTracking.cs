using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessonDemo
{
    using Models;

    public class ObjectStateTracking
    {
        /*
         * Когато търсим бързодействие и няма да променяме entry-то
         * добра практика да е в състояние Detach - с метода AsNoTrackinh()
         * 
         * 
         * */

        //Change tracking
        public static void ChangeTracker(MusicXContext context)
        {
            //Change tracker follow changes on this object
            //but this rule only applies when object exist in Models in DBContext            
            var song = context.Songs.FirstOrDefault(x => x.Name == "Осъдени души");
            song.Name = "Промени песента"; 
            song.ModifiedOn = DateTime.UtcNow;

            //When select(проекция) is execute, change tracker not recognize this object
            var objectNotExistInCOntext = context.Songs
                .Where(x => x.Name.StartsWith("Осъдени души"))
                .Select(x => new NotExistInContext { Id = x.Id, Name = x.Name })
                .FirstOrDefault();

            context.SaveChanges();
        }

        //Attached or Detached condition(tracked object)
        public static void ObjectCondition(MusicXContext context)
        {
            //Attached statement by default
            var Attached = context.Songs.FirstOrDefault(x => x.Name.Contains("Осъдени души"));
            Attached.Name = "Промени песента";
            Attached.ModifiedOn = DateTime.UtcNow;
            context.SaveChanges();

            //Detached statement
            var Detached = context.Songs
                .AsNoTracking()
                .FirstOrDefault(x => x.Name.Contains("Осъдени души"));

            Detached.Name = "Промени песента";
            Detached.ModifiedOn = DateTime.UtcNow;

            //Set context expressly detach mode
            context.Entry(Attached).State = EntityState.Detached;
            Attached.Name = "Промени песента";
            Attached.ModifiedOn = DateTime.UtcNow;

            context.SaveChanges();



        }

        //When we want to update a detached object we need to
        //reattach it and then update it:
        public static void ChangeStateObject(MusicXContext context)
        {
            var DetachedSongOject = context.Songs
                .AsNoTracking()
                .FirstOrDefault(x => x.Name.Contains("Осъдени души"));

            var entry = context.Entry(DetachedSongOject);
            entry.State = EntityState.Modified;
            DetachedSongOject.Name = "New name";

            context.SaveChanges();

        }

    }

    internal class NotExistInContext
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
