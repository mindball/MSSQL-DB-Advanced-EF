using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Services.Models
{
    public class DistrictViewModel
    {
        public string Name { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }

        public double AvgPrice { get; set; }

        public int PropertiesCount { get; set; }

    }
}
