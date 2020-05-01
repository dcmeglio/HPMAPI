using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.GraphQL.Types
{
    public class DriverType : ObjectGraphType<HPMAPI.Entities.Driver>
    {
        public DriverType()
        {
            Field(x => x.id);
            Field(x => x.name);
            Field(x => x.@namespace);

            Field<ListGraphType<AlternateNameType>>("alternateNames");
            Field(x => x.location);
            Field(x => x.required);
            Field(x => x.version, nullable: true);
        }
    }
}
