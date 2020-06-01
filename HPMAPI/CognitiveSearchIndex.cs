using AutoMapper;
using HPMAPI.Configuration;
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
        private IMapper automapper { get; set; }
        public CognitiveSearchIndex(IOptions<AzureSettings> settings, IMapper mapper)
        {
            try
            {
                automapper = mapper;
                azureSettings = settings.Value;
                serviceClient = new SearchServiceClient(azureSettings.SearchServiceName, new SearchCredentials(azureSettings.SearchServiceAPIKey));
                if (!serviceClient.Indexes.Exists(azureSettings.SearchServiceIndex))
                    serviceClient.Indexes.Create(new Index(azureSettings.SearchServiceIndex, FieldBuilder.BuildForType<CognitiveSearchPackage>()));
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
        public async Task AddRepository(Repository repository)
        {
            List<Package> packagesToDelete = new List<Package>();
            try
            {
                var item = await cosmosContainer.ReadItemAsync<Repository>(repository.id, new PartitionKey(repository.location));

                foreach (var package in item.Resource.packages)
                {
                    if (!repository.packages.Any(x => x.location == package.location))
                        packagesToDelete.Add(package);
                }

                await cosmosContainer.UpsertItemAsync(repository, new PartitionKey(repository.location));
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
            try
            {
                var items = automapper.Map<IList<Package>, IList<CognitiveSearchPackage>>(repository.packages);
                foreach (var item in items)
                {
                    item.author = repository.author;
                }
                var batch = IndexBatch.MergeOrUpload<CognitiveSearchPackage>(items);
                searchClient.Documents.Index(batch);
                if (packagesToDelete.Any())
                {
                    batch = IndexBatch.Delete(automapper.Map<List<Package>,List<CognitiveSearchPackage>>(packagesToDelete));
                    await searchClient.Documents.IndexAsync(batch);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine($"Error adding repository {repository.location}: {e}");
                return;
            }
        }

        public async Task<IEnumerable<string>> Search(string searchString)
        {
            try
            {
                var result = await searchClient.Documents.SearchAsync<CognitiveSearchPackage>(searchString);
                return automapper.Map<List<CognitiveSearchPackage>, List<Package>>(result.Results.Select(d => d.Document).ToList()).Select(x => x.location);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine($"Exception during search {searchString} {e}");
                return null;
            }
        }

        public async Task<IEnumerable<string>> Search(string searchString, int? offset, int? size)
        {
            offset ??= 0;
            var searchParams = new SearchParameters
            {
                Skip = offset,
                Top = size
            };
            try
            {
                var result = await searchClient.Documents.SearchAsync<CognitiveSearchPackage>(searchString, searchParams);
                return automapper.Map<List<CognitiveSearchPackage>, List<Package>>(result.Results.Select(d => d.Document).ToList()).Select(x => x.location);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine($"Exception during search {searchString} {e}");
                return null;
            }
        }
    }
}
