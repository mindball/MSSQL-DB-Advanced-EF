

namespace CarDealer.Imports.FileProcessing
{
    using System;
    using System.IO;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using CarDealer.Contracts;
    using CarDealer.Data;
    using CarDealer.Models;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class JsonProcess : IJsonProcess
    {

        private readonly CarDealerContext context;
        private readonly string fullPath;
        private readonly string loadedFile;
        private JToken token;        

        public JsonProcess(string fullPath)
        {
            this.context = new CarDealerContext();

            if(this.IsValidJson(fullPath))
                this.fullPath = fullPath;

            this.loadedFile = File.ReadAllText(this.fullPath);            
        }        

        public string LoadedFile => this.loadedFile;

        public CarDealerContext Context => this.context;

        public string Path => this.fullPath;        

        public abstract void Import();  

        public bool IsValidJson(string fullPath)
        {
            var strInput = File.ReadAllText(fullPath);

            if (string.IsNullOrWhiteSpace(strInput)) { return false; }

            strInput = strInput.Trim();

            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    this.token = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
