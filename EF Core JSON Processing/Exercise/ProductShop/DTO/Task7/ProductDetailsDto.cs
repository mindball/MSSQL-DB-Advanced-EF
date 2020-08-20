using Newtonsoft.Json;

namespace ProductShop.DTO.Task7
{
    public class ProductDetailsDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
