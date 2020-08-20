using Newtonsoft.Json;

namespace ProductShop.DTO.Category
{
    public class GetAllCategoryWithProductCount
    {
        [JsonProperty("category")]
        public string Name { get; set; }

        [JsonProperty("productCount")]
        public int ProductCount { get; set; }

        [JsonProperty("averagePrice")]
        public decimal AveragePrice { get; set; }

        [JsonProperty("totalRevenue")]
        public decimal TotalPrice { get; set; }

        //Може да ползваме закръгляне до две числа след дес. запетая
        //описва се в profile-CreateMap .ToString("f2")
        //[JsonProperty("averagePrice")]
        //public string StringAveragePrice { get; set; }

        //[JsonProperty("totalRevenue")]
        //public string StringTotalPrice { get; set; }

    }
}
