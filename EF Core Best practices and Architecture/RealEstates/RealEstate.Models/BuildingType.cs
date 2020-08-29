using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class BuildingType
    {
        public BuildingType()
        {
            this.RealEstateProperties = new List<RealEstateProperty>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<RealEstateProperty> RealEstateProperties { get; set; }
    }
}