using GraphQL;
using GraphQL.Types;
using HPMAPI.GraphQL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.GraphQL.Schemas
{
    public class RepositorySchema : Schema
    {
        public RepositorySchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<RepositoryQuery>();
        }
    }
}
