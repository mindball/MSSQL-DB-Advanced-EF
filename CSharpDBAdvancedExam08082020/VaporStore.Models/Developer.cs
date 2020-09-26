using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.Models
{
    public class Developer
    {
        public Developer()
        {
            this.Games = new List<Game>();
        }
        public int Id { get; set; }

        [Required]        
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
