using HPMAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPMAPI.Repositories
{
    public interface IRepositories
    {
        IEnumerable<Repository> GetAll();
        Task<IEnumerable<Repository>> GetAllAsync();

        Repository GetByName(string name);
        Task<Repository> GetByNameAsync(string name);
    }
}
