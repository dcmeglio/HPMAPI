using GraphQL.Types;

namespace HPMAPI.GraphQL.Types
{
    public class ManifestType : ObjectGraphType<Entities.Manifest>
    {
        public ManifestType()
        {
            Field(x => x.packageName);
            Field(x => x.documentationLink, nullable: true);
            Field(x => x.communityLink, nullable: true);
            Field(x => x.releaseNotes, nullable: true);
            Field(x => x.licenseFile, nullable: true);
            Field(x => x.author);
            Field(x => x.dateReleased);
            Field(x => x.minimumHEVersion);
            Field(x => x.version);
            Field(x => x.betaVersion, nullable: true);

            Field<ListGraphType<DriverType>>("drivers");
            Field<ListGraphType<AppType>>("apps");
        }
    }
}