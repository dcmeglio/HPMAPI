using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.Entities
{
    public class App
    {
        public string id { get; set; }
        public string name { get; set; }
        public string @namespace { get; set; }
        public IList<AlternateName> alternateNames { get; set; }
        public string location { get; set; }
        public bool required { get; set; }
        public bool oauth { get; set; }
        public string version { get; set; }
    }
}
