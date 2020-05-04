using HPMAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPMAPI.Repositories
{
    public interface IRepositories
    {
        List<Repository> GetAll();
        Task<List<Repository>> GetAllAsync();
    }
}
