using HPMAPI.Configuration;
using HPMAPI.Entities;
using HPMAPI.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HPMAPI.Repositories
{
    public class Repositories : IRepositories
    {
        private HPMSettings settings;
        public Repositories(IOptions<HPMSettings> hpmSettings)
        {
            settings = hpmSettings.Value;
        }
        public List<Repository> GetAll()
        {
            WebClient wc = new WebClient();
            var repoliststr = wc.DownloadString(settings.RepositoryListing);
            JObject repoList = JObject.Parse(repoliststr);
            var repos = repoList["repositories"].ToObject<IEnumerable<Repository>>().ToList();
            List<Repository> results = new List<Repository>();
            foreach (var repo in repos)
            {
                wc = new WebClient();
                var repostr = wc.DownloadString(repo.location);

                var repoFileContents = JsonConvert.DeserializeObject<Repository>(repostr);
                repoFileContents.name = repo.name;
                repoFileContents.location = repo.location;
                results.Add(repoFileContents);
            }
            return results;
        }

        public async Task<List<Repository>> GetAllAsync()
        {
            WebClient wc = new WebClient();
            var repoliststr = await wc.DownloadStringTaskAsync(settings.RepositoryListing);
            JObject repoList = JObject.Parse(repoliststr);
            var repos = repoList["repositories"].ToObject<IEnumerable<Repository>>().ToList();
            List<Repository> results = new List<Repository>();
            foreach (var repo in repos)
            {
                wc = new WebClient();
                var repostr = await wc.DownloadStringTaskAsync(repo.location);

                var repoFileContents = JsonConvert.DeserializeObject<Repository>(repostr);
                repoFileContents.name = repo.name;
                repoFileContents.location = repo.location;
                results.Add(repoFileContents);
            }
            return results;
        }
    }
}
