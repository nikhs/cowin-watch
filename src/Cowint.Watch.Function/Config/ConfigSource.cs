using Cowin.Watch.Core;
using System;
using System.Collections.Generic;

namespace Cowin.Watch.Function.Config
{

    internal static class ConfigKeys
    {
        public const string SearchByVaccine = "SearchByVaccine";
        public const string CowinBaseUrl = "CowinBaseUrl";
        public const string DistrictId = "DistrictId";
        public const string Pincode = "Pincode";
    }

    internal abstract class ConfigSource
    {
        protected abstract string? GetConfigValue(string key);

        public VaccineType? SearchByVaccine()
        {
            var envValue = GetConfigValue(ConfigKeys.SearchByVaccine);
            if (String.IsNullOrWhiteSpace(envValue)) {
                return null;
            }
            return VaccineType.From(envValue);
        }

        public Uri CowinBaseUrl()
        {
            var envValue = GetConfigValue(ConfigKeys.CowinBaseUrl) ?? throw new ArgumentNullException(ConfigKeys.CowinBaseUrl);
            return new Uri(envValue);
        }

        public DistrictId? DistrictId()
        {
            var envValue = GetConfigValue(ConfigKeys.DistrictId);
            if (String.IsNullOrWhiteSpace(envValue)) {
                return null;
            }
            return Core.DistrictId.FromInt(int.Parse(envValue));
        }

        public Pincode? Pincode()
        {
            var envValue = GetConfigValue(ConfigKeys.Pincode);
            if (String.IsNullOrWhiteSpace(envValue)) {
                return null;
            }
            return Core.Pincode.FromString(envValue);
        }
    }

    internal class EnvironmentConfigSource : ConfigSource
    {
        protected override string? GetConfigValue(string key) => Environment.GetEnvironmentVariable(key);
        public static EnvironmentConfigSource Get() => new EnvironmentConfigSource();
    }

    internal class InmemoryConfigSource : ConfigSource
    {
        private readonly Dictionary<string, string> InMemoryMap;
        public InmemoryConfigSource(Dictionary<string, string> configMap)
        {
            InMemoryMap = configMap;
        }

        protected override string? GetConfigValue(string key) => InMemoryMap.GetValueOrDefault(key, string.Empty);
    }
}
