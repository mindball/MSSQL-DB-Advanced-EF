using CarDealer.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;

namespace CarDealer
{
    class StartUp
    {
        static void Main(string[] args)
        {        
            Engine engine = new Engine();            
            engine.Run();
        }
        
    }
}
