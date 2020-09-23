using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int DeveloperId { get; set; }
        [Required]
        public virtual Developer Developer { get; set; }

        [Required]
        public int GenreId { get; set; }
        [Required]
        public virtual Genre Genre { get; set; }
                
        public virtual ICollection<Purchase> Purchases { get; set; }

        [Required]
        public virtual ICollection<GameTags> GamesTags { get; set; }



    }
}
