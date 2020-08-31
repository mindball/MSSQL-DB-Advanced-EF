using RealEstate.Services.Models;
using System.Collections.Generic;

namespace RealEstate.Services
{
    public interface IPropertiesService
    {
        void Create(int size, int? floor, int maxFloors, 
            string district, string propertyType, 
            string buildinType, int? year, int price);

        void UpdateTags(int propertyId);

        IEnumerable<PropertyViewModel> Search(int minYear, int maxYear, int minSize, int maxSize);

        IEnumerable<PropertyViewModel> SearchByPrice(int minPrice, int maxPrice);

    }
}
