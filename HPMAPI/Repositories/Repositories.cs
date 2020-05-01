using HPMAPI.Entities;
using HPMAPI.Interfaces;
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
        private IIndexer LuceneIndex;
        public Repositories(IIndexer luceneIndex)
        {
            LuceneIndex = luceneIndex;


        }
        public List<Repository> GetAll()
        {
            WebClient wc = new WebClient();
            var repoliststr = wc.DownloadString("https://raw.githubusercontent.com/dcmeglio/hubitat-packagerepositories/master/repositories.json");
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
    }
}
