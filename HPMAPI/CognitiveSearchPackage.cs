using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System.Text.Json.Serialization;

namespace HPMAPI
{
    public class CognitiveSearchPackage
    {
        [IsSearchable]
        [Analyzer(AnalyzerName.AsString.EnMicrosoft)]
        public string name { get; set; }

        [IsSearchable]
        [Analyzer(AnalyzerName.AsString.EnMicrosoft)]
        public string category { get; set; }

        [IsSearchable]
        [Analyzer(AnalyzerName.AsString.EnMicrosoft)]
        public string secondaryCategory { get; set; }

        [System.ComponentModel.DataAnnotations.Key]
        [IsFilterable, IsRetrievable(true)]
        [JsonConverter(typeof(Base64JsonConverter))]
        public string location { get; set; }

        [IsSearchable]
        [Analyzer(AnalyzerName.AsString.EnMicrosoft)]
        public string description { get; set; }
        [IsSearchable]
        [Analyzer(AnalyzerName.AsString.EnMicrosoft)]
        public string author { get; set; }

        public string betaLocation { get; set; }
    }
}
