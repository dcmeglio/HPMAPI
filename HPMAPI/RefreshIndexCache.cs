using HPMAPI.Interfaces;
using HPMAPI.Repositories;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HPMAPI
{
    public class RefreshIndexCache : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private Timer timer;
        private IIndexer indexer;
        private IRepositories repositories;

        public RefreshIndexCache(IRepositories repo, IIndexer index)
        {
            indexer = index;
            repositories = repo;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            lock (this)
            {
                indexer.DeleteAll();
                var repos = repositories.GetAll();
                foreach (var repo in repos)
                {
                    indexer.AddPackages(repo.packages);
                }
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
