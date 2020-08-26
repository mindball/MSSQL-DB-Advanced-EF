using Newtonsoft.Json;

namespace CarDealer.DTO.CarsWithTheirListOfParts
{
    public class CarInfoList
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }
    }
}
