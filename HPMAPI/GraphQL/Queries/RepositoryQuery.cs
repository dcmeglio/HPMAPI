using GraphQL.Types;
using HPMAPI.GraphQL.Helpers;
using HPMAPI.GraphQL.Types;
using HPMAPI.Interfaces;
using HPMAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HPMAPI.GraphQL.Queries
{
    public class RepositoryQuery : ObjectGraphType
    {
        public RepositoryQuery(IRepositories repositories, IPackages packages, IIndexer indexer)
        {
            FieldAsync<ListGraphType<RepositoryType>>(
               "repositories",
               arguments: new QueryArguments(
                        new QueryArgument<IntGraphType> { Name = "size" },
                        new QueryArgument<IntGraphType> { Name = "offset" }),
               resolve: async context => {
                   var offset = context.GetArgument<int?>("offset");
                   var size = context.GetArgument<int?>("size");
                   var results = await repositories.GetAllAsync();
                   return GraphQLHelpers.PageResults(results, size, offset);
               }
           );

            FieldAsync<RepositoryType>(
               "repository",
               arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "name" }),
               resolve: async context =>
               {
                   return await repositories.GetByNameAsync(context.GetArgument<string>("name"));
               });

            FieldAsync<PackageType>(
                "package",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "name" }),
               resolve: async context =>
               {
                   return await packages.GetByNameAsync(context.GetArgument<string>("name"));
               });

            FieldAsync<ListGraphType<PackageType>>(
               "packages",
               arguments: new QueryArguments(
                        new QueryArgument<StringGraphType> { Name = "category" },
                        new QueryArgument<ListGraphType<StringGraphType>> { Name = "tags" },
                        new QueryArgument<StringGraphType> { Name = "name" },
                        new QueryArgument<StringGraphType> { Name = "location" },
                        new QueryArgument<StringGraphType> { Name = "search" },
                        new QueryArgument<IntGraphType> { Name = "size" },
                        new QueryArgument<IntGraphType> { Name = "offset" }),
               resolve: async context =>
               {
                   IEnumerable<Entities.Package> results = await packages.GetAllAsync();
                   var category = context.GetArgument<string>("category");
                   var name = context.GetArgument<string>("name");
                   var location = context.GetArgument<string>("location");
                   var search = context.GetArgument<string>("search");
                   var tags = context.GetArgument<List<string>>("tags");
                   var offset = context.GetArgument<int?>("offset");
                   var size = context.GetArgument<int?>("size");
                   if (category != null)
                       results = results.Where(x => x.category?.Equals(category, StringComparison.InvariantCultureIgnoreCase) == true);
                   if (tags != null)
                       results = results.Where(r => r.tags?.Any(t => tags.Any(st => st.Equals(t, StringComparison.InvariantCultureIgnoreCase))) == true);
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
