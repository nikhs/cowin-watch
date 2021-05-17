using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Cowin.Watch.Core.Tests.Lib
{
    public static class CentersEnumerable
    {
        public static IEnumerable<Center> GetFor(string json) => Deserialize(json);
        private static IEnumerable<Center> Deserialize(string json) => JsonSerializer.Deserialize<Root>(json)?.Centers;
    }
}
