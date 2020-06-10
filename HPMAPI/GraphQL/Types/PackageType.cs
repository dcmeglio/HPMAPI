using GraphQL.Types;

namespace HPMAPI.GraphQL.Types
{
    public class PackageType : ObjectGraphType<Entities.Package>
    {
        public PackageType()
        {
            Field(x => x.name);
            Field(x => x.category);
            Field(x => x.location);
            Field(x => x.betaLocation, nullable: true);
            Field(x => x.description);
            Field(x => x.tags, nullable: true);
        }
    }
}
