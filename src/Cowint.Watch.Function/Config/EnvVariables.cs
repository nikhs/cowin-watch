using Cowin.Watch.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cowin.Watch.Function.Config
{
    public static class EnvVariables
    {
        public static readonly string KEY_SearchByVaccine = "SearchByVaccine";
        public static readonly string KEY_CowinBaseUrl = "CowinBaseUrl";

        public static VaccineType SearchByVaccine()
        {
            var envValue = Environment.GetEnvironmentVariable(KEY_SearchByVaccine);
            if (String.IsNullOrWhiteSpace(envValue)) {
                return null;
            }
            return VaccineType.From(envValue);
        }

        public static Uri CowinBaseUrl()
        {
            var envValue = Environment.GetEnvironmentVariable(KEY_CowinBaseUrl);
            return new Uri(envValue);
        }

    }
}
