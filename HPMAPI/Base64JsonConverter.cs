using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI
{
    public class Base64JsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                string str = (string)reader.Value;

                byte[] byteBuffer = Convert.FromBase64String(str);
                return System.Text.Encoding.UTF8.GetString(byteBuffer);
            }
            catch
            {
                return reader.Value;
            }
        }

        //convert bitmap to byte (serialize)
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string val = (string)value;
            
            writer.WriteValue(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(val)));
        }
    }
}
