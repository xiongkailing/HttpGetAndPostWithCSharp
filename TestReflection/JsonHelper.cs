using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HttpFaker
{
    public class JsonHelper
    {
        public static T JsonDeserialize<T>(string json)
           where T : class
        {

            T t = JsonConvert.DeserializeObject<T>(json);
            return t;
        }

        public static string JsonSerializer<T>(T t)
        {
            return JsonConvert.SerializeObject(t);
        }

        public static JObject ConvertJobject(string json)
        {
            return JObject.Parse(json);
        }
    }
}