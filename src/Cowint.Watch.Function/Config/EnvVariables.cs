using Cowin.Watch.Core;
using System;

namespace Cowin.Watch.Function.Config
{
    public static class EnvVariables
    {
        static readonly string KEY_SearchByVaccine = "SearchByVaccine";
        static readonly string KEY_CowinBaseUrl = "CowinBaseUrl";
        static readonly string KEY_DistrictId = "DistrictId";
        static readonly string KEY_Pincode = "Pincode";

        internal static VaccineType SearchByVaccine()
        {
            var envValue = Environment.GetEnvironmentVariable(KEY_SearchByVaccine);
            if (String.IsNullOrWhiteSpace(envValue)) {
                return null;
            }
            return VaccineType.From(envValue);
        }

        internal static Uri CowinBaseUrl()
        {
            var envValue = Environment.GetEnvironmentVariable(KEY_CowinBaseUrl);
            return new Uri(envValue);
        }

        internal static DistrictId DistrictId()
        {
            var envValue = Environment.GetEnvironmentVariable(KEY_DistrictId);
            return Core.DistrictId.FromInt(int.Parse(envValue));
        }

        internal static Pincode Pincode()
        {
            var envValue = Environment.GetEnvironmentVariable(KEY_Pincode);
            if (String.IsNullOrWhiteSpace(envValue)) {
                return null;
            }
            return Core.Pincode.FromString(envValue);
        }
    }
}
