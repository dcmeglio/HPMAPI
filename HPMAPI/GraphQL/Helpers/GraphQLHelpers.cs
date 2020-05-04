using System.Collections.Generic;
using System.Linq;

namespace HPMAPI.GraphQL.Helpers
{
    public static class GraphQLHelpers
    {
        public static IEnumerable<T> PageResults<T>(IEnumerable<T> input, int? size, int? offset)
        {
            var results = input;
            if (offset != null)
                results = results.Skip(offset.Value);

            if (size != null)
                results = results.Take(size.Value);

            return results;
        }
    }
}
