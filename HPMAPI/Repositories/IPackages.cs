using HPMAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPMAPI.Repositories
{
    public interface IPackages
    {
        IEnumerable<Package> GetAll();
        Task<IEnumerable<Package>> GetAllAsync();
        Package GetByName(string name);
        Task<Package> GetByNameAsync(string name);
    }
}
