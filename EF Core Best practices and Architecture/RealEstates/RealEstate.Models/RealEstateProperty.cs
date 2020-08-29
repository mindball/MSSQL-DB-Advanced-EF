﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Models
{
    public class RealEstateProperty
    {
        public RealEstateProperty()
        {
            this.Tags = new List<Tag>();
        }
        public int Id { get; set; }

        public int Size { get; set; }

        public int? Floor { get; set; }

        public int? TotalNumberOfFloors { get; set; }

        public int DistrictId { get; set; }

        public virtual District District { get; set; }

        public int? Year { get; set; }

        public int PropertyTypeId { get; set; }

        public virtual PropertyType PropertyType { get; set; }

        public int BuildingTypeId { get; set; }

        public virtual BuildingType BuildingType { get; set; }

        public int Price { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}