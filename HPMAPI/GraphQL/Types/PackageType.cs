using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.GraphQL.Types
{
    public class PackageType : ObjectGraphType<HPMAPI.Entities.Package>
    {
        public PackageType()
        {
            Field(x => x.name);
            Field(x => x.category);
            Field(x => x.location);
            Field(x => x.description);
        }
    }
}
