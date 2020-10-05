using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.Services.Models
{
    public class GamesViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Developer { get; set; }

        public IList<TagViewModel> Tags { get; set; }

        public int Players { get; set; }
    }
}
