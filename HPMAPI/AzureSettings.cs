using HPMAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI
{
    public class AzureSettings
    {
        public string SearchServiceName { get; set; }

        public string SearchServiceAPIKey { get; set; }

        public string SearchServiceIndex { get; set; }
    }
}
