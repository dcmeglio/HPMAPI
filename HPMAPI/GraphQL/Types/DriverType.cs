using GraphQL.Types;

namespace HPMAPI.GraphQL.Types
{
    public class DriverType : ObjectGraphType<Entities.Driver>
    {
        public DriverType()
        {
            Field(x => x.id);
            Field(x => x.name);
            Field(x => x.@namespace);

            Field<ListGraphType<AlternateNameType>>("alternateNames");
            Field(x => x.location);
            Field(x => x.required);
            Field(x => x.version, nullable: true);
            Field(x => x.betaVersion, nullable: true);
            Field(x => x.betaLocation, nullable: true);
        }
    }
}
