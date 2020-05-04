using GraphQL.Types;

namespace HPMAPI.GraphQL.Types
{
    public class AlternateNameType : ObjectGraphType<Entities.AlternateName>
    {
        public AlternateNameType()
        {
            Field(x => x.name);
            Field(x => x.@namespace);
        }
    }
}
