using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealEstate.Models
{
    public class Tag
    {
        public Tag()
        {
            this.RealEstateProperties = new List<RealEstateProperty>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public  virtual ICollection<RealEstateProperty> RealEstateProperties { get; set; }
    }
}
