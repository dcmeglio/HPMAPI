using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.GraphQL.Types
{
    public class Manifest : ObjectGraphType<HPMAPI.Entities.Manifest>
    {
        public Manifest()
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

            Field<ListGraphType<Driver>>("drivers");
            Field<ListGraphType<App>>("apps");
        }
    }
}