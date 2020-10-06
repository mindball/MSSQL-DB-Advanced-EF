﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.Services.Models
{
    public class GenreViewModel
    {
        public int Id { get; set; }

        public string Genre { get; set; }        

        public List<GamesViewModel> Games { get; set; }

        //public int PlayersCount { get; set; }
    }
}