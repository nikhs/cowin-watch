using System;
using System.Collections.Generic;
using System.Linq;

namespace Cowin.Watch.Core
{
    public interface IFinderFilter
    {
        DateTimeOffset DateFrom { get; }

        IEnumerable<Center> Filter(Root result);
    }

    internal abstract class FinderFilterBase : IFinderFilter
    {
        public DateTimeOffset DateFrom { get; }

        protected FinderFilterBase(DateTimeOffset dateFrom)
        {
            DateFrom = dateFrom;
        }

        public IEnumerable<Center> Filter(Root result)
        {
            return DefaultFilter(result)
                .Where(AdditionalCenterFilter);
        }

        private readonly Func<Root, IEnumerable<Center>> DefaultFilter = delegate (Root result) {
            if (result == null || result.Centers == null || result.Centers.Count == 0) {
                return Enumerable.Empty<Center>();
            }

            if (result.Centers.All(c => c.Sessions == null || c.Sessions.Count == 0)) {
                return Enumerable.Empty<Center>();
            }

            return result.Centers
                .Where(center => center.Sessions
                .Any(session => session.AvailableCapacity > 0));
        };

        protected abstract Func<Center, bool> AdditionalCenterFilter { get; }
    }

    internal class DateOnlyFinder : FinderFilterBase
    {
        public DateOnlyFinder(DateTimeOffset dateFrom) : base(dateFrom)
        {

        }

        protected override Func<Center, bool> AdditionalCenterFilter => (center) => true;
    }

    internal class VaccineFinder : FinderFilterBase
    {
        private readonly VaccineType vaccineType;

        public VaccineFinder(VaccineType vaccineType, DateTimeOffset dateFrom) : base(dateFrom)
        {
            this.vaccineType = vaccineType ?? throw new ArgumentNullException(nameof(vaccineType));
        }

        protected override Func<Center, bool> AdditionalCenterFilter => delegate (Center center) {
            return center.Sessions
            .Any(session => vaccineType.Equals(session?.Vaccine ?? String.Empty) && session?.Slots?.Count > 0);
        };
    }
}