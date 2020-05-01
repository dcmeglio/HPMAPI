using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.Interfaces
{
    public interface IIndexer
    {
        public void AddPackages(IList<Entities.Package> pkg);
        public void DeleteAll();
        List<string> Search(string searchString);


    }
}
