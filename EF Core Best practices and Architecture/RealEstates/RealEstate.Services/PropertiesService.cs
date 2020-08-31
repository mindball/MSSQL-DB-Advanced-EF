using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
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
                Year = year,
                Floor = floor,
                TotalNumberOfFloors = maxFloors
            };

            if(year == 1800)            
                property.Year = null;            

            if (floor <= 0)
                property.Floor = null;

            if (maxFloors <= 0)
                property.TotalNumberOfFloors = 0;

            //District
            var districtEntity = this.context.Districts
                .FirstOrDefault(d => d.Name.Trim() == district.Trim());            
            if(districtEntity == null)
            {
                districtEntity = new District { Name = district };
            }

            property.District = districtEntity;

            //Property Type
            var propertyTypeEntity = this.context.PropertyTypies
                .FirstOrDefault(x => x.Name.Trim() == propertyType.Trim());
            if(propertyTypeEntity == null)
            {
                propertyTypeEntity = new PropertyType { Name = propertyType };
            }

            property.PropertyType = propertyTypeEntity;

            //Building Type
            var buildintTypeEntity = this.context.BuildingTypes
                .FirstOrDefault(bt => bt.Name.Trim() == buildinType.Trim());
            if(buildintTypeEntity == null)
            {
                buildintTypeEntity = new BuildingType { Name = buildinType };
            }

            property.BuildingType = buildintTypeEntity;

            //TODO: Tags
            this.context.RealEstateProperties.Add(property);
            this.context.SaveChanges();

            this.UpdateTags(property.Id);

        }

        public void UpdateTags(int propertyId)
        {
            var property = this.context.RealEstateProperties
                .FirstOrDefault(x => x.Id == propertyId);

            property.Tags.Clear();

            if(property.Year.HasValue && property.Year < 1990)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("OldBuilding")
                    });
            }

            if (property.Size > 120)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("HugeApartment")
                    });
            }

            if (property.Year > 2018 && property.TotalNumberOfFloors > 5)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("HasParking")
                    });
            }

            if (property.Floor == property.TotalNumberOfFloors)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("LastFloor")
                    });
            }

            if ((property.Price / property.Size) < 800)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("CheapProperty")
                    });
            }

            if ((property.Price / property.Size) > 2000)
            {
                property.Tags.Add(
                    new RealEstatePropertyTag
                    {
                        Tag = this.GetOrCreateTag("ExpensiveProperty")
                    });
            }

            this.context.SaveChanges();
        }

        private Tag GetOrCreateTag(string tag)
        {
            var tagEntity =this.context.Tags.FirstOrDefault(t => t.Name.Trim() == tag.Trim());

            if(tagEntity == null)
            {
                tagEntity = new Tag { Name = tag };
            }

            return tagEntity;
        }

        public IEnumerable<PropertyViewModel> Search(int minYear, int maxYear, int minSize, int maxSize)
        {
            return this.context.RealEstateProperties
                .Where(x => x.Year >= minYear && x.Year <= maxYear
                && x.Size >= minSize && x.Size <= maxSize)
                .Select(MapToPropertyViewModel())
                .OrderBy(x => x.Price)
                .ToList();
        }

        private static Expression<Func<RealEstateProperty, PropertyViewModel>> MapToPropertyViewModel()
        {
            return x => new PropertyViewModel
            {
                Price = x.Price,
                Floor = (x.Floor ?? 0).ToString() + "/" + (x.TotalNumberOfFloors ?? 0),
                Size = x.Size,
                Year = x.Year,
                BuildingType = x.BuildingType.Name,
                District = x.District.Name,
                PropertyType = x.PropertyType.Name
            };
        }

        public IEnumerable<PropertyViewModel> SearchByPrice(int minPrice, int maxPrice)
        {
            return this.context.RealEstateProperties
                .Where(x => x.Price >= minPrice && x.Price <= maxPrice)
                .Select(MapToPropertyViewModel())
                .OrderBy(x => x.Price)
                .ToList();
        }
    }
}
