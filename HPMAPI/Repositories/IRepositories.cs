using HPMAPI.Entities;
using System.Collections.Generic;

namespace HPMAPI.Repositories
{
    public interface IRepositories
    {
        List<Repository> GetAll();
    }
}
