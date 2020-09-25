using System;
using System.Collections.Generic;

namespace VaporStore.DataProcessor
{
    public class JsonGame
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Developer { get; set; }

        public string Genre { get; set; }

        public List<string> Tags { get; set; }
    }
}
