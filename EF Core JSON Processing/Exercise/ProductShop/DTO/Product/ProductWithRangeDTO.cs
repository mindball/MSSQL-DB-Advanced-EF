using Newtonsoft.Json;

namespace ProductShop.DTO.Product
{
    public class ProductWithRangeDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("seller")]
        public string SellerName { get; set; }
    }
}
