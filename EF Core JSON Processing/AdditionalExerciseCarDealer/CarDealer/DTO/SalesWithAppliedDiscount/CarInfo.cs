using Newtonsoft.Json;

namespace CarDealer.DTO.SalesWithAppliedDiscount
{
    public class CarInfo
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }
    }
}
