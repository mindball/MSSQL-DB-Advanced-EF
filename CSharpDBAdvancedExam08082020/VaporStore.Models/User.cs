using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.Models
{
    public class User
    {
        private const int Min_Len = 3;

        public int Id { get; set; }

        [Required]
        [MaxLength(20), MinLength(Min_Len)]
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z][a-z]+\s[A-Z]+[a-z]+$")]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [MaxLength(10), MinLength(Min_Len)]
        public int Age { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
