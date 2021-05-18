using Cowin.Watch.Core;
using Cowin.Watch.Function.Config;
using System;

namespace Cowin.Watch.Function
{
    public interface IFunctionConfig
    {
        IFinderConstraint GetConstraint();

        IFinderFilter GetFilter();
    }

    internal class PrioritizeDistrictAndVaccine : IFunctionConfig
    {
        private readonly ConfigSource configSource;

        public PrioritizeDistrictAndVaccine(ConfigSource configSource) => this.configSource = configSource;

        public IFinderConstraint GetConstraint()
        {
            return (configSource.DistrictId(), configSource.Pincode()) switch {
                (DistrictId districtId, _) => FinderConstraintFactory.From(districtId),
                (null, Pincode pincode) => FinderConstraintFactory.From(pincode),
                _ => throw new NotSupportedException(),
            };
        }

        public IFinderFilter GetFilter()
        {
            return configSource.SearchByVaccine() switch {
                VaccineType vaccineType => FinderFilterFactory.From(DateTime.Now, vaccineType),
                _ => FinderFilterFactory.From(DateTime.Now),
            };
        }
    }
}