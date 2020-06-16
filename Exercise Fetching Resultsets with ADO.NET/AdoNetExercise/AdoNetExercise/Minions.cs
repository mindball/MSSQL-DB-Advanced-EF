using System;
using System.Collections.Generic;
using System.Text;

namespace AdoNetExercise
{
    public class Minions
    {
        public int Id { get; set; }

        public string Name { get; set; }

        private int EvilnessFactorId { get; set; }

        public int? Age { get; set; }

        public long RowNumber { get; set; }

        public int? TownId { get; set; }
    }
}
