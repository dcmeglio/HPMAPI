using GraphQL.Types;

namespace HPMAPI.GraphQL.Types
{
    public class AppType : ObjectGraphType<Entities.App>
    {
        public AppType()
        {
            Field(x => x.id);
            Field(x => x.name);
            Field(x => x.@namespace);
            Field(x => x.alternateNames);
            Field(x => x.location);
            Field(x => x.required);
            Field(x => x.oauth, nullable: true);
            Field(x => x.version, nullable: true);
            Field(x => x.betaVersion, nullable: true);
            Field(x => x.betaLocation, nullable: true);
        }
    }
}
