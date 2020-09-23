using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        public string  Name { get; set; }

        public virtual ICollection<GameTags> GameTags { get; set; }
    }
}
