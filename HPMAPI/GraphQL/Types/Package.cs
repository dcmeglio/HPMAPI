using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.GraphQL.Types
{
    public class Package : ObjectGraphType<HPMAPI.Entities.Package>
    {
        public Package()
        {
            Field(x => x.name);
            Field(x => x.category);
            Field(x => x.location);
            Field(x => x.description);
        }
    }
}
