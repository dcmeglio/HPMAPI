using HPMAPI.Entities;
using HPMAPI.Interfaces;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Index = Microsoft.Azure.Search.Models.Index;

namespace HPMAPI
{
    public class CognitiveSearchIndex : IIndexer
    {
        private AzureSettings azureSettings { get; set; }
        private SearchServiceClient serviceClient { get; set; }
        private SearchIndexClient searchClient { get; set; }
        public CognitiveSearchIndex(IOptions<AzureSettings> settings)
        {
            azureSettings = settings.Value;
            serviceClient = new SearchServiceClient(azureSettings.SearchServiceName, new SearchCredentials(azureSettings.SearchServiceAPIKey));
            
            
        }
        public void AddPackages(IList<Package> pkgs)
        {
            var batch = IndexBatch.Upload<Package>(pkgs);
            searchClient.Documents.Index(batch);
        }

        public void DeleteAll()
        {
            if (serviceClient.Indexes.Exists(azureSettings.SearchServiceIndex))
            {
                serviceClient.Indexes.Delete(azureSettings.SearchServiceIndex);
            }
            var definition = new Index()
            {
                Name = azureSettings.SearchServiceIndex,
                Fields = FieldBuilder.BuildForType<Package>()
                
                
            };
            
            serviceClient.Indexes.Create(definition);
            searchClient = new SearchIndexClient(azureSettings.SearchServiceName, azureSettings.SearchServiceIndex, new SearchCredentials(azureSettings.SearchServiceAPIKey));
        }

        public List<string> Search(string searchString)
        {
            var result = searchClient.Documents.Search<Package>(searchString).Results.Select(x => x.Document.location).ToList();
            return result;
        }
    }
}
