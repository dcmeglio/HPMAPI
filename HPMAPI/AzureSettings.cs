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
        public string CosmosEndpoint { get; set; }
        public string CosmosKey { get; set; }
        public string CosmosDatabase { get; set; }
        public string CosmosContainer { get; set; }
    }
}
