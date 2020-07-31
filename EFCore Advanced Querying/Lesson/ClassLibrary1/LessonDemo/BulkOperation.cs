using LessonDemo.Models;
using System.Linq;
using Z.EntityFramework.Plus;

namespace LessonDemo
{
    public class BulkOperation
    {
        //Entity Framework does not support bulk operations
        //Z.EntityFramework.Plus gives you the ability to perform bulk
            //update/delete of entities
        //Install-Package Z.EntityFramework.Plus.EFCore

        public static void SomeBulkOperation(MusicXContext context)
        {
            //Delete
            context.SongMetadata
                .Where(x => x.SongId <= 10)
                .Delete();

            //Update
            context.Songs
                .Where(x => x.Name.Contains("а") || x.Name.Contains("е"))
                .Update(song => new Songs { Name = song.Name + "  (BG)" });
        }

    }
}
