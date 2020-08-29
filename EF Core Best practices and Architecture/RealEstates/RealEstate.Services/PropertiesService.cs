using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Services
{
    public class PropertiesService : IPropertiesService
    {
        private RealEstateDbContext context;

        public PropertiesService(RealEstateDbContext context)
        {
            this.context = context;
        }
        public void Create(int size, int? floor, int maxFloors, string district, string propertyType, string buildinType, int? year, int price)
        {
            var property = new RealEstateProperty()
            {
                Size = size,
                Price = price,
            };

            if(year == 1800)            
                property.Year = null;            

            if (floor <= 0)
                property.Floor = null;


        }

        public IEnumerable<PropertyViewModel> Search(int minYear, int maxYear, int minSize, int MaxSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PropertyViewModel> SearchByPrice(int minPrice, int maxPrice)
        {
            throw new NotImplementedException();
        }
    }
}
