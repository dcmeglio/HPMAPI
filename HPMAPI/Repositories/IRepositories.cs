using HPMAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.Repositories
{
    public interface IRepositories
    {
        List<Repository> GetAll();
    }
}
