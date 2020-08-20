using Newtonsoft.Json;

namespace ProductShop.DTO.Task7
{
    public class SoldProductDto
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("products")]

        public ProductDetailsDto[] Products { get; set; }
    }
}
