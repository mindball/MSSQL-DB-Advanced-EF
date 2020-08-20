using Newtonsoft.Json;

namespace ProductShop.DTO.Task7
{
    public class UserDetailsDto
    {
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("age")]
        public int? Age { get; set; }

        [JsonProperty("soldProducts")]
        public SoldProductDto SoldProducts { get; set; }
    }
}
