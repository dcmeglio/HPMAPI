using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.GraphQL.Types
{
    public class App : ObjectGraphType<HPMAPI.Entities.App>
    {
        public App()
        {
            Field(x => x.id);
            Field(x => x.name);
            Field(x => x.@namespace);
            Field(x => x.alternateNames);
            Field(x => x.location);
            Field(x => x.required);
            Field(x => x.oauth, nullable: true);
            Field(x => x.version, nullable: true);
        }
    }
}
