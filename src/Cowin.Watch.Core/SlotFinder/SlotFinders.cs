using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core
{
    internal class SlotFinderByDistrictId : ISlotFinder
    {
        private readonly CowinApiHttpClient cowinApiHttpClient;
        public readonly DistrictId districtId;

        public SlotFinderByDistrictId(CowinApiHttpClient cowinApiHttpClient, DistrictId districtId)
        {
            this.cowinApiHttpClient = cowinApiHttpClient ?? throw new ArgumentNullException(nameof(cowinApiHttpClient));
            this.districtId = districtId ?? throw new ArgumentNullException(nameof(districtId));
        }

        public async Task<IEnumerable<Center>> FindBy(IFinderFilter finderFilter, CancellationToken none)
        {
            var result = await cowinApiHttpClient
                .GetSessionsForDistrictAndDateAsync(districtId, finderFilter.DateFrom, CancellationToken.None);
            return finderFilter.Filter(result);
        }
    }

    public class SlotFinderByPincode : ISlotFinder
    {
        private readonly CowinApiHttpClient cowinApiHttpClient;
        public readonly Pincode pincode;

        public SlotFinderByPincode(CowinApiHttpClient cowinApiHttpClient, Pincode pincode)
        {
            this.cowinApiHttpClient = cowinApiHttpClient ?? throw new ArgumentNullException(nameof(cowinApiHttpClient));
            this.pincode = pincode ?? throw new ArgumentNullException(nameof(pincode));
        }

        
        public async Task<IEnumerable<Center>> FindBy(IFinderFilter finderFilter, CancellationToken none)
        {
            var result = await cowinApiHttpClient
                .GetSessionsForPincodeAndDateAsync(pincode, finderFilter.DateFrom, CancellationToken.None);
            return finderFilter.Filter(result);
        }
    }
}
