using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VaporStore.Models.Enums;

namespace VaporStore.Models
{
    public class Card
    {
        private const int Min_Len = 3;

        public Card()
        {
            this.Purchases = new List<Purchase>();
        }

        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{4}\s[0-9]{4}\s[0-9]{4}\s[0-9]{4}$")]
        public int Number { get; set; }

        [MaxLength(Min_Len), MinLength(Min_Len)]
        public int Cvc { get; set; }

        [Required]
        public CardType Type { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public virtual User User { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
