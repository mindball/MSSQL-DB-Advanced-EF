using System;
using System.ComponentModel.DataAnnotations;
using VaporStore.Models.Enums;

namespace VaporStore.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        [Required]
        public PurchaseType Type { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$")]
        public string ProductKey { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int CardId { get; set; }
        [Required]
        public virtual Card Card { get; set; }

        [Required]
        public int GameId { get; set; }
        [Required]
        public virtual Game Game { get; set; }
    }
}
