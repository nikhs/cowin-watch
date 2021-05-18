using Cowin.Watch.Function.Config;
using System.Collections.Generic;

namespace Cowin.Watch.Function
{
    public static class FunctionConfigFactory
    {
        public static IFunctionConfig FromMemory(string districtId, string pincode, string vaccine)
        {
            return new PrioritizeDistrictAndVaccine(new InmemoryConfigSource(new Dictionary<string, string>() {
                {ConfigKeys.DistrictId, districtId },
                {ConfigKeys.Pincode, pincode },
                {ConfigKeys.SearchByVaccine, vaccine}
            }));
        }

        public static IFunctionConfig FromEnvironment() => new PrioritizeDistrictAndVaccine(EnvironmentConfigSource.Get());
    }
}