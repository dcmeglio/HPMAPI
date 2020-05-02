using HPMAPI.Entities;
using HPMAPI.Interfaces;
using Microsoft.Azure.Cosmos;
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
        private CosmosClient cosmosClient { get; set; }
        private Database cosmosDatabase { get; set; }
        private Container cosmosContainer { get; set; }
        public CognitiveSearchIndex(IOptions<AzureSettings> settings)
        {
            try
            {
                azureSettings = settings.Value;
                serviceClient = new SearchServiceClient(azureSettings.SearchServiceName, new SearchCredentials(azureSettings.SearchServiceAPIKey));
                if (!serviceClient.Indexes.Exists(azureSettings.SearchServiceIndex))
                    serviceClient.Indexes.Create(new Index(azureSettings.SearchServiceIndex, FieldBuilder.BuildForType<Package>()));
                searchClient = new SearchIndexClient(azureSettings.SearchServiceName, azureSettings.SearchServiceIndex, new SearchCredentials(azureSettings.SearchServiceAPIKey)); 
                cosmosClient = new CosmosClient(azureSettings.CosmosEndpoint, azureSettings.CosmosKey);
                cosmosDatabase = cosmosClient.GetDatabase(azureSettings.CosmosDatabase);
                cosmosContainer = cosmosClient.GetContainer(azureSettings.CosmosDatabase, azureSettings.CosmosContainer);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine($"Error connecting to Azure services for index: {e}");
            }
        }
        public void AddRepository(Repository repository)
        {
            List<Package> packagesToDelete = new List<Package>();
            try
            {
                var item = cosmosContainer.ReadItemAsync<dynamic>(repository.id, new PartitionKey(repository.location)).GetAwaiter().GetResult();

                foreach (var package in item.Resource.packages)
                {
                    if (!repository.packages.Any(x => x.location == package.location))
                        packagesToDelete.Add(package);
                }

                cosmosContainer.UpsertItemAsync(repository, new PartitionKey(repository.location)).GetAwaiter().GetResult();
                var batch = IndexBatch.MergeOrUpload<Package>(repository.packages);
                searchClient.Documents.Index(batch);
                if (packagesToDelete.Any())
                {
                    batch = IndexBatch.Delete(packagesToDelete);
                    searchClient.Documents.Index(batch);
                }
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // All good, swallow the exception.
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine($"Error adding repository {repository.location}: {e}");
                return;
            }
        }

        public IEnumerable<string> Search(string searchString)
        {
            try
            {
                var result = searchClient.Documents.Search<Package>(searchString).Results.Select(x => x.Document.location);
                return result;
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine($"Exception during search {searchString} {e}");
                return null;
            }
        }
    }
}
