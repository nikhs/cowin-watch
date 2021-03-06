using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core
{
    internal class SlotFinderByDistrictId : ISlotFinder
    {
        private readonly CowinApiHttpClient cowinApiHttpClient;
        public readonly DistrictId districtId;

        private SlotFinderByDistrictId(CowinApiHttpClient cowinApiHttpClient, DistrictId districtId)
        {
            this.cowinApiHttpClient = cowinApiHttpClient ?? throw new ArgumentNullException(nameof(cowinApiHttpClient));
            this.districtId = districtId ?? throw new ArgumentNullException(nameof(districtId));
        }

        public async Task<ICentersResponse> FindBy(IFinderFilter finderFilter, CancellationToken cancellationToken)
        {
            var result = await cowinApiHttpClient
                .GetSessionsForDistrictAndDateAsync(districtId, finderFilter.DateFrom, cancellationToken);
            return CentersResponseFactory.GetFor(finderFilter.Filter(result));
        }

        public static SlotFinderByDistrictId From(CowinApiHttpClient cowinApiHttpClient, SearchByDistrictConstraint searchByDistrictConstraint) =>
            new SlotFinderByDistrictId(cowinApiHttpClient, searchByDistrictConstraint.DistrictId);
    }

    internal class SlotFinderByPincode : ISlotFinder
    {
        private readonly CowinApiHttpClient cowinApiHttpClient;
        public readonly Pincode pincode;

        private SlotFinderByPincode(CowinApiHttpClient cowinApiHttpClient, Pincode pincode)
        {
            this.cowinApiHttpClient = cowinApiHttpClient ?? throw new ArgumentNullException(nameof(cowinApiHttpClient));
            this.pincode = pincode ?? throw new ArgumentNullException(nameof(pincode));
        }

        public async Task<ICentersResponse> FindBy(IFinderFilter finderFilter, CancellationToken cancellationToken)
        {
            var result = await cowinApiHttpClient
                .GetSessionsForPincodeAndDateAsync(pincode, finderFilter.DateFrom, cancellationToken);
            return CentersResponseFactory.GetFor(finderFilter.Filter(result));
        }

        public static SlotFinderByPincode From(CowinApiHttpClient cowinApiHttpClient, SearchByPincodeConstraint searchByPincodeConstraint) =>
            new SlotFinderByPincode(cowinApiHttpClient, searchByPincodeConstraint.Pincode);
    }
}
