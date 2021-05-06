using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core
{
    public class SlotFinderByDistrictId
    {
        private readonly CowinApiHttpClient cowinApiHttpClient;
        public readonly DistrictId districtId;

        public SlotFinderByDistrictId(CowinApiHttpClient cowinApiHttpClient, DistrictId districtId)
        {
            this.cowinApiHttpClient = cowinApiHttpClient ?? throw new ArgumentNullException(nameof(cowinApiHttpClient));
            this.districtId = districtId ?? throw new ArgumentNullException(nameof(districtId));
        }

        public async Task<IEnumerable<Center>> GetAvailableSlotsForDateAsync(DateTimeOffset from)
        {
            var result = await cowinApiHttpClient
                .GetSessionsForDistrictAndDateAsync(districtId, from, CancellationToken.None);

            if (result == null || result.Centers == null || result.Centers.Count == 0) {
                return Enumerable.Empty<Center>();
            }

            if (result.Centers.All(c => c.Sessions == null || c.Sessions.Count == 0)) {
                return Enumerable.Empty<Center>();
            }

            return result.Centers
                .Where(center => center.Sessions
                .Any(session => session.Slots.Count > 0));
        }

        public async Task<IEnumerable<Center>> GetAvailableSlotsForDateAndVaccineAsync(DateTimeOffset from, VaccineType expectedVaccine)
        {
            var result = await cowinApiHttpClient
                .GetSessionsForDistrictAndDateAsync(districtId, from, CancellationToken.None);
            expectedVaccine.Equals(string.Empty);

            if (result == null || result.Centers == null || result.Centers.Count == 0) {
                return Enumerable.Empty<Center>();
            }

            if (result.Centers.All(c => c.Sessions == null || c.Sessions.Count ==0)) {
                return Enumerable.Empty<Center>();
            }

            return result.Centers
                .Where(center => center.Sessions
                .Any(session => expectedVaccine.Equals(session.Vaccine) && session.Slots.Count > 0 ));
        }
    }
}
