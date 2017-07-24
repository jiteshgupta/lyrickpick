using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Web;

namespace LyrickPick.Processors
{
    public class DataParser
    {
        private string jsonData = String.Empty;

        public DataParser(string jsonData)
        {
            this.jsonData = jsonData;
        }

        public List<string[]> parseData(string jsonData, string[] inputFields)
        {
            List<string[]> items = new List<string[]>();

            var jsonObject = JObject.Parse(jsonData);

            return items;
        }

        public List<string[]> parseData(string[] inputFields)
        {
            return parseData(jsonData, inputFields);
        }
    }
}