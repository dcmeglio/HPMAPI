using HPMAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.Repositories
{
    public class Packages : IPackages
    {
        private IRepositories repositories;
        public Packages(IRepositories repos)
        {
            repositories = repos;
        }
        public IEnumerable<Package> GetAll()
        {
            return repositories.GetAll().SelectMany(x => x.packages);
        }

        public async Task<IEnumerable<Package>> GetAllAsync()
        {
            return (await repositories.GetAllAsync()).SelectMany(x => x.packages);
        }

        public Package GetByName(string name)
        {
            return repositories.GetAll().SelectMany(x => x.packages).SingleOrDefault(x => x.name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<Package> GetByNameAsync(string name)
        {
            return (await repositories.GetAllAsync()).SelectMany(x => x.packages).SingleOrDefault(x => x.name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
