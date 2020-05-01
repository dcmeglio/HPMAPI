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
            Field<ListGraphType<RepositoryType>>(
               "repositories",
               resolve: context => repositories.GetAll()
           );

            Field<RepositoryType>(
               "repository",
               arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "name" }),
               resolve: context =>
               {
                   return repositories.GetAll().SingleOrDefault(x => 
                        x.name.Equals(context.GetArgument<string>("name"), StringComparison.InvariantCultureIgnoreCase)
                    );
               }
           );

        }
    }
}
