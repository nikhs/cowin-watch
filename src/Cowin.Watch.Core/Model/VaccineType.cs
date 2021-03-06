using System;
using System.Collections.Generic;
using System.Linq;

namespace Cowin.Watch.Core
{
    public sealed class VaccineType : IEquatable<string>, IEquatable<VaccineType>
    {
        private const string VACCINE_STR_COVIDSHIELD = "covishield";
        private const string VACCINE_STR_COVAXIN = "covaxin";

        private static readonly VaccineType COVIDSHIELD = new VaccineType(VACCINE_STR_COVIDSHIELD);
        private static readonly VaccineType COVAXIN = new VaccineType(VACCINE_STR_COVAXIN);
        public static IEnumerable<VaccineType> All => new List<VaccineType>() { COVIDSHIELD, COVAXIN };

        private readonly string vaccineType;

        private VaccineType(string vaccineType)
        {
            if (string.IsNullOrWhiteSpace(vaccineType)) {
                throw new ArgumentNullException(nameof(vaccineType), $"'{nameof(vaccineType)}' cannot be null or whitespace");
            }

            this.vaccineType = vaccineType;
        }

        public static VaccineType CovidShield() => COVIDSHIELD;
        public static VaccineType Covaxin() => COVAXIN;

        public static VaccineType From(string vaccineType)
        {
            if (All.Any(vaccine => vaccine.Equals(vaccineType))) {
                return All.Single(vaccine => vaccine.Equals(vaccineType));
            }
            throw new ArgumentOutOfRangeException($"{vaccineType} is not a supported vaccineType!");
        }

        public static bool IsCovidShield(string vaccine) =>
            vaccine.Equals(VACCINE_STR_COVIDSHIELD, StringComparison.OrdinalIgnoreCase);

        public static bool IsCovaxin(string vaccine) =>
            vaccine.Equals(VACCINE_STR_COVAXIN, StringComparison.OrdinalIgnoreCase);

        public bool IsCovidShield() => IsCovidShield(this.vaccineType);
        public bool IsCovaxin() => IsCovaxin(this.vaccineType);

        public bool Equals(string other) => this.vaccineType.Equals(other, StringComparison.OrdinalIgnoreCase);
        public bool Equals(VaccineType other) => other.Equals(this.vaccineType);
        public override bool Equals(object obj) => Equals((VaccineType)obj);
        public override int GetHashCode() => this.vaccineType.GetHashCode();

        public override string ToString() => vaccineType;

    }
}