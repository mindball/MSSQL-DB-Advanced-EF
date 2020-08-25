namespace CarDealer.DTO
{
    using Newtonsoft.Json;
    using System;

    public class OrderedCustomer
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("BirthDate")]        
        public DateTime BirthDate { get; set; }

        [JsonProperty("IsYoungDriver")]
        public bool IsYoungDriver { get; set; }

    }
}
