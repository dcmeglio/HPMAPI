using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.GraphQL.Types
{
    public class Driver : ObjectGraphType<HPMAPI.Entities.Driver>
    {
        public Driver()
        {
            Field(x => x.id);
            Field(x => x.name);
            Field(x => x.@namespace);

            Field<ListGraphType<AlternateName>>("alternateNames");
            Field(x => x.location);
            Field(x => x.required);
            Field(x => x.version, nullable: true);
        }
    }
}
