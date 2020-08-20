using Newtonsoft.Json;

namespace ProductShop.DTO.User
{
    public class UserNameWithSoldItemsDTO
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("soldProducts")]
        public UserSoldItemDTO[] SoldItems { get; set; }
    }
}
