using Newtonsoft.Json;

namespace CarDealer.DTO.SalesWithAppliedDiscount
{
    public class SaleDTO
    {
        [JsonProperty("car")]
        public CarInfo CarInfo { get; set; }

        [JsonProperty("customerName")]
        public string Name { get; set; }

        public decimal Discount { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("priceWithDiscount")]
        public decimal PriceWithDiscount { get; set; }
    }
}
