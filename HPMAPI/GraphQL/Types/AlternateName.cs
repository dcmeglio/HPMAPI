using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.GraphQL.Types
{
    public class AlternateName : ObjectGraphType<HPMAPI.Entities.AlternateName>
    {
        public AlternateName()
        {
            Field(x => x.name);
            Field(x => x.@namespace);
        }
    }
}
