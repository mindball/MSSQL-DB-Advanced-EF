using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO.TotalSalesByCustomer
{
    public class CustomerSales
    {
        [JsonProperty("fullName")]
        public string Name { get; set; }

        [JsonProperty("boughtCars")]
        public int BoughCarsCount { get; set; }

        [JsonProperty("spentMoney")]
        public string SpentMoney { get; set; }
    }
}
