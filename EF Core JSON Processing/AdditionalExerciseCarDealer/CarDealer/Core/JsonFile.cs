using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace CarDealer.Core
{
    public class JsonFile
    {
        private readonly string path;
        private JToken token;

        public JsonFile(string path)
        {
            this.path = path;
        }
        public string ValidJsonFile 
        { 
            get
            {
                if (this.IsValidJson(this.path))
                {
                    return token.ToString();
                }
                else
                {
                    return "Not valid json file";
                }
            }
        }

        private bool IsValidJson(string path)
        {
            var strInput = File.ReadAllText(path);            

            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            
            strInput = strInput.Trim();

            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    token = JToken.Parse(strInput);                    
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
