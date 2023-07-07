using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Extensions
{
    public static class JsonExtension
    {
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize<T>(string value, JsonSerializerSettings settings = null)
        {
            if (settings == null)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return JsonConvert.DeserializeObject<T>(value, settings);
        }
    }
}
