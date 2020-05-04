using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;

namespace HPMAPI.Entities
{
    public class Package
    {
        [IsSearchable]
        [Analyzer(AnalyzerName.AsString.EnMicrosoft)]
        public string name { get; set; }
        [IsSearchable]
        [Analyzer(AnalyzerName.AsString.EnMicrosoft)]
        public string category { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        [IsFilterable, IsRetrievable(true)]
        [JsonConverter(typeof(Base64JsonConverter))]
        public string location { get; set; }
        [IsSearchable]
        [Analyzer(AnalyzerName.AsString.EnMicrosoft)]
        public string description { get; set; }
    }
}
