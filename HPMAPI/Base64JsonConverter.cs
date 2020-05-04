using Newtonsoft.Json;
using System;

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
            // There is something funny that goes on in the way this is called by Azure Cognitive Search where sometimes it's a 
            // b64 string and sometimes it isn't. I'm not totally sure why, but this safely decodes it if it is b64, but leaves 
            // it alone if it isn't.
            if (reader.Value == null)
                return reader.Value;

            string str = (string)reader.Value;
            Span<byte> buffer = new Span<byte>(new byte[str.Length]);
            if (Convert.TryFromBase64String(str, buffer, out _))
            {
                return System.Text.Encoding.UTF8.GetString(buffer.ToArray());
            }
            else
                return reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string val = (string)value;
            
            writer.WriteValue(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(val)));
        }
    }
}
