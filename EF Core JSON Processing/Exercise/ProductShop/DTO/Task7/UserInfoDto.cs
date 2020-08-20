using Newtonsoft.Json;

namespace ProductShop.DTO.Task7
{
    public class UserInfoDto
    {
        [JsonProperty("userCount")]
        public int Count { get; set; }

        [JsonProperty("users")]
        public UserDetailsDto[] Users { get; set; }
    }
}
