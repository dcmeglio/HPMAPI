using System.Collections.Generic;

namespace HPMAPI.Entities
{
    public class App
    {
        public string id { get; set; }
        public string name { get; set; }
        public string @namespace { get; set; }
        public IList<AlternateName> alternateNames { get; set; }
        public string location { get; set; }
        public string betaLocation { get; set; }
        public bool required { get; set; }
        public bool oauth { get; set; }
        public string version { get; set; }
        public string betaVersion { get; set; }
        public bool primary { get; set; }
    }
}
