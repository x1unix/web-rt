using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebRT.Foundation
{
    class Bundle
    {
        private Dictionary<string, string> Data;

        public void PutObject(string key, object value)
        {
            Data.Add(key, JsonConvert.SerializeObject(value));
        }

        public void PutString(string key, string value)
        {
            Data.Add(key, value);
        }

        public void PutInteger(string key, int value)
        {
            Data.Add(key, value.ToString());
        }

        public object GetObject(string key) {
            return JsonConvert.DeserializeObject(Data[key]);
        }
    }
}
