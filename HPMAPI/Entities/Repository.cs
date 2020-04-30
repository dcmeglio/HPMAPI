using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.Entities
{
    public class Repository
    {
        public string name { get; set; }
        public string location { get; set; }
        public string author { get; set; }
        public string gitHubUrl { get; set; }
        public string payPalUrl { get; set; }
        public IList<Package> packages { get; set; }
    }
}
