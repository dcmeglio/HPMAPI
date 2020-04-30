﻿using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.GraphQL.Types
{
    public class Repository : ObjectGraphType<HPMAPI.Entities.Repository>
    {
        public Repository()
        {
            Field(x => x.name);
            Field(x => x.location);
            Field(x => x.author);
            Field(x => x.gitHubUrl, nullable: true);
            Field(x => x.payPalUrl, nullable: true);

            Field<ListGraphType<Package>>("packages",
                     arguments: new QueryArguments(
                        new QueryArgument<StringGraphType> { Name = "category", DefaultValue = null },
                        new QueryArgument<StringGraphType> { Name = "name", DefaultValue = null },
                        new QueryArgument<IntGraphType> { Name = "size", DefaultValue = null },
                        new QueryArgument<IntGraphType> { Name = "offset", DefaultValue = null }),
                     resolve: context =>
                     {
                         IEnumerable<Entities.Package> results = context.Source.packages;
                         var category = context.GetArgument<string>("category");
                         var name = context.GetArgument<string>("name");
                         var offset = context.GetArgument<int?>("offset");
                         var size = context.GetArgument<int?>("size");
                         if (category != null)
                             results = results.Where(x => x.category.Equals(category, StringComparison.InvariantCultureIgnoreCase));
                         if (name != null)
                             results = results.Where(x => x.name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                         if (offset != null)
                             results = results.Skip(offset.Value);
                         if (size != null)
                             results = results.Take(size.Value);
                         return results;
                     }); ;


        }
    }
}

