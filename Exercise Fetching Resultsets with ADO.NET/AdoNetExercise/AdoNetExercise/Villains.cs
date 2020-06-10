using System;
using System.Collections.Generic;
using System.Text;

namespace AdoNetExercise
{
    class Villains
    {
        /*Write a program that prints on the console all villains’ 
         * names and their number of minions of those who have more
        than 3 minions ordered descending by number of minions.
        **/

        public int Id { get; set; }

        public string Name { get; set; }

        private  int  EvilnessFactorId { get; set; }

        public int CountOfMinios { get; set; }

    }
}
