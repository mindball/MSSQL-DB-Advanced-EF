using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer.DTO.CarsWithTheirListOfParts
{
    public class CarDTO
    {
        //[JsonProperty("car")]
        //public string Name { get; set; }

        [JsonProperty("car")]
        public CarInfoList InfoCar { get; set; }

        [JsonProperty("parts")]
        public ListOfParts[] Parts { get; set; }
    }
}
