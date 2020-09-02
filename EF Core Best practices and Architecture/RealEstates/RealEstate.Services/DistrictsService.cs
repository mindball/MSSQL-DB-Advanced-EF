namespace RealEstate.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using RealEstate.Data;
    using RealEstate.Models;
    using RealEstate.Services.Models;

    public class DistrictsService : IDistrictsService
    {
        private RealEstateDbContext context;

        public DistrictsService(RealEstateDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DistrictViewModel> GetTopDistrictByAveragePrice(int count = 10)
        {
            return this.context.Districts
                .OrderByDescending(x => x.RealEstateProperties
                                .Average(s => (double)s.Price / s.Size))
                .Select(MapToDistrictViewModel())
                .Take(count)
                .ToList();
        }

        public IEnumerable<DistrictViewModel> GetTopDistrictByNumberOfProperties(int count = 10)
        {
            return this.context.Districts
                .OrderByDescending(x => x.RealEstateProperties.Count())
                .Select(MapToDistrictViewModel())
                .Take(count)
                .ToList();
        }

        private static Expression<Func<District, DistrictViewModel>> MapToDistrictViewModel()
        {
            return x => new DistrictViewModel
            {
                AvgPrice = x.RealEstateProperties
                                            .Average(s => (double)s.Price / s.Size),
                MinPrice = x.RealEstateProperties
                                            .Min(x => x.Price),
                MaxPrice = x.RealEstateProperties
                                            .Max(x => x.Price),
                Name = x.Name,
                PropertiesCount = x.RealEstateProperties.Count()
            };
        }
    }
}