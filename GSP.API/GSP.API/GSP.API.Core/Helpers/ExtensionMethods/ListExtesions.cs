using System.Collections.Generic;
using System.Linq;

namespace GSP.API.Core.Helpers.ExtensionMethods
{
    public static class ListExtesions
    {
        public static bool HasValue<T>(this IEnumerable<T> list) => list?.ToList() is { Count: > 0 } is true;
    }
}
