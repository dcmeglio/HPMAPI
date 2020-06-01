using Newtonsoft.Json;

namespace HPMAPI.Entities
{
    public class Package
    {
        public string name { get; set; }
        public string category { get; set; }
        public string secondaryCategory { get; set; }
        [JsonConverter(typeof(Base64JsonConverter))]
        public string location { get; set; }
        public string description { get; set; }
        public string betaLocation { get; set; }
    }
}
