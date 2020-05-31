using GraphQL.Types;
using HPMAPI.GraphQL.Helpers;
using HPMAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HPMAPI.GraphQL.Types
{
    public class RepositoryType : ObjectGraphType<Entities.Repository>
    {
        public RepositoryType(IIndexer indexer)
        {
            Field(x => x.name);
            Field(x => x.location);
            Field(x => x.author);
            Field(x => x.gitHubUrl, nullable: true);
            Field(x => x.payPalUrl, nullable: true);
            
            FieldAsync<ListGraphType<PackageType>>("packages",
                     arguments: new QueryArguments(
                        new QueryArgument<StringGraphType> { Name = "category" },
                        new QueryArgument<StringGraphType> { Name = "name" },
                        new QueryArgument<StringGraphType> { Name = "location" },
                        new QueryArgument<StringGraphType> { Name = "search" },
                        new QueryArgument<IntGraphType> { Name = "size" },
                        new QueryArgument<IntGraphType> { Name = "offset" }),
                     resolve: async context =>
                     {
                         IEnumerable<Entities.Package> results = context.Source.packages;
                         var category = context.GetArgument<string>("category");
                         var name = context.GetArgument<string>("name");
                         var location = context.GetArgument<string>("location");
                         var search = context.GetArgument<string>("search");
                         var offset = context.GetArgument<int?>("offset");
                         var size = context.GetArgument<int?>("size");
                         if (category != null)
                             results = results.Where(x => x.category.Equals(category, StringComparison.InvariantCultureIgnoreCase) || x.secondaryCategory.Equals(category, StringComparison.InvariantCultureIgnoreCase));
                         if (name != null)
                             results = results.Where(x => x.name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                         if (location != null)
                             results = results.Where(x => x.location == location);

                         if (search != null)
                         {
                             var matches = await indexer.Search(search, offset, size);
                             if (matches.Any())
                                 return results.Where(x => matches.Contains(x.location));
                             else
                                 return null;
                         }
                         return GraphQLHelpers.PageResults(results, size, offset);
                     });
        }
    }
}

