using System;

namespace Demo
{
    public class WeatherForeCast
    {
        public DateTime DateTime { get; set; } = DateTime.UtcNow.Date;

        public int TemperatureC { get; set; } = 30;

        public string Summary { get; set; } = "Hot Summer Day";

        public override string ToString()
        {
            return $"Date:{this.DateTime.ToString()} " +
                $"Temp:{this.TemperatureC} Summary:{this.Summary}"; 
        }

    }
}
