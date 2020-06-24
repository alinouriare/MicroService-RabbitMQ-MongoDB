using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Tools
{
    public static class Serializer
    {
        public static byte[] ToByteArray(this object obj)
        {
            if (obj ==null)
            {
                return null;
            }
            var json = JsonConvert.SerializeObject(obj);
            return Encoding.ASCII.GetBytes(json);
        }

        public static T ToType<T>(this byte[] byteArray)
        {
            var json = Encoding.Default.GetString(byteArray);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string DeserializeToText(this byte[] byteArray)
        {
            return Encoding.Default.GetString(byteArray);
        }
    }
}
