using GraphQL;
using GraphQL.Types;
using HPMAPI.GraphQL.Queries;

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
