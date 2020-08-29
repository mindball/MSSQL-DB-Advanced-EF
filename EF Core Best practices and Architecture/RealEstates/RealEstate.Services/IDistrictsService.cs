namespace RealEstate.Services
{
    using Models;
    using System.Collections.Generic;

    public interface IDistrictsService
    {
        IEnumerable<DistrictViewModel> GetTopDistrictByAveragePrice(int count = 10);

        IEnumerable<DistrictViewModel> GetTopDistrictByNumberOfProperties(int count = 10);

    }
}
