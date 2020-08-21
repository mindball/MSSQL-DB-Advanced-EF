using CarDealer.Core;
using System;
using System.Linq.Expressions;

namespace CarDealer
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine();
            var t = engine.IsValidJson("../../../Datasets/cars.json");
        }
    }
}
