using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class PropertyType
    {
        public PropertyType()
        {
            this.RealEstateProperties = new List<RealEstateProperty>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<RealEstateProperty> RealEstateProperties { get; set; }
    }
}