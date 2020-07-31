using System;
using System.Collections.Generic;
using System.Text;

namespace LessonDemo
{
    class CascadeOperations
    {
        /*
         * Required FK with cascade delete set to true, deletes
            everything related to the deleted property

         * Required FK with cascade delete set to false, throws exception
            (it cannot leave the navigational property with no value)

         * Optional FK with cascade delete set to true, deletes everything
                related to the deleted property.

         * Optional FK with cascade delete set to false, sets the value of
            the FK to NULL
        */

        // Using OnDelete with DeleteBehavior Enumeration:
        //DeleteBehavior.Cascade
        //Deletes related entities(default for required FK)

        //DeleteBehavior.Restrict
        //Throws exception on delete

        //DeleteBehavior.SetNull
        //Sets the property to null (affects database)

            /* Some Code:
             * 
            modelBuilder.Entity<User>()
                .HasMany(u => u.Replies)
                .WithOne(a => a.Author)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Replies)
                .WithOne(a => a.Author)
                .OnDelete(DeleteBehavior.Cascade);

            */

    }
}
