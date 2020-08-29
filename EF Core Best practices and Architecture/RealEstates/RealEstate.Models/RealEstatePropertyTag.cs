﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Models
{
    public class RealEstatePropertyTag
    {
        public int RealEstatePropertyTagId { get; set; }

        public int RealEstatePropertyId { get; set; }

        public virtual RealEstateProperty RealEstateProperty { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
