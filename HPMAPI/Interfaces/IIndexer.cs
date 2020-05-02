using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.Interfaces
{
    public interface IIndexer
    {
        public void AddRepository(Entities.Repository repo);
        IEnumerable<string> Search(string searchString);


    }
}
