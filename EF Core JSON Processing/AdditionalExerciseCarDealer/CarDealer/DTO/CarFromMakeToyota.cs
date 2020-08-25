﻿using Newtonsoft.Json;
using System;

namespace CarDealer.DTO
{
    public class CarFromMakeToyota
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public long  TravelledDistance  { get; set; }
    }
}
