using HPMAPI.Configuration;
using HPMAPI.Interfaces;
using HPMAPI.Repositories;
using HPMAPI.Utilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HPMAPI
{
    public class RefreshIndexCache : IHostedService, IDisposable
    {
        private Task timer;
        private IIndexer indexer;
        private IRepositories repositories;
        private HPMSettings settings;

        public RefreshIndexCache(IRepositories repo, IIndexer index, IOptions<HPMSettings> hpmSettings)
        {
            indexer = index;
            repositories = repo;
            settings = hpmSettings.Value;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            timer = PeriodicTask.Run(() => DoWork(), TimeSpan.Zero, TimeSpan.FromHours(settings.IndexRebuildFrequency), stoppingToken);
            return timer;
        }

        private void DoWork()
        {
            List<Task> tasks = new List<Task>();
            lock (this)
            {
                var repos = repositories.GetAll();
                foreach (var repo in repos)
                {
                    tasks.Add(indexer.AddRepository(repo));
                }
                Task.WaitAll(tasks.ToArray());
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            return timer;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
