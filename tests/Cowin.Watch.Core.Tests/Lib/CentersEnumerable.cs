using System.Collections.Generic;
using System.Text.Json;

namespace Cowin.Watch.Core.Tests.Lib
{
    public static class CentersEnumerable
    {
        public static IEnumerable<Center> GetFor(string json) => Deserialize(json);
        public static IEnumerable<Center> GetEmpty() => new List<Center>();
        private static IEnumerable<Center> Deserialize(string json) => JsonSerializer.Deserialize<Root>(json)?.Centers;
    }
}
