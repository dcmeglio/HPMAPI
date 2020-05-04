using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPMAPI.Interfaces
{
    public interface IIndexer
    {
        public Task AddRepository(Entities.Repository repo);
        Task<IEnumerable<string>> Search(string searchString);
        Task<IEnumerable<string>> Search(string searchString, int? offset, int? size);
    }
}
