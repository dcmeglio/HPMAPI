using GraphQL;
using GraphQL.Types;
using HPMAPI.GraphQL.Types;
using HPMAPI.Interfaces;
using HPMAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.GraphQL.Queries
{
    public class RepositoryQuery : ObjectGraphType
    {
        public RepositoryQuery(IRepositories repositories)
        {
            FieldAsync<ListGraphType<RepositoryType>>(
               "repositories",
               resolve: async context => await repositories.GetAllAsync()
           );

            FieldAsync<RepositoryType>(
               "repository",
               arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "name" }),
               resolve: async context =>
               {
                   return (await repositories.GetAllAsync()).SingleOrDefault(x => 
                        x.name.Equals(context.GetArgument<string>("name"), StringComparison.InvariantCultureIgnoreCase)
                    );
               }
           );

        }
    }
}
