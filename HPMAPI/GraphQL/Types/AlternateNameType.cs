using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.GraphQL.Types
{
    public class AlternateNameType : ObjectGraphType<HPMAPI.Entities.AlternateName>
    {
        public AlternateNameType()
        {
            Field(x => x.name);
            Field(x => x.@namespace);
        }
    }
}
