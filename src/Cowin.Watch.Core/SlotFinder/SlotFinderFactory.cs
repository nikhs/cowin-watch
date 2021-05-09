using Cowin.Watch.Core.SlotFinder;
using System;

namespace Cowin.Watch.Core
{
    public class SlotFinderFactory
    {
        public static ISlotFinder For(CowinApiHttpClient cowinApiClient, IFinderConstraint finderConstraint)
        {
            return finderConstraint switch {
                SearchByDistrictConstraint searchByDistrictConstraint => new SlotFinderByDistrictId(cowinApiClient, searchByDistrictConstraint.DistrictId),
                SearchByPincodeConstraint searchByPincodeConstraint => new SlotFinderByPincode(cowinApiClient, searchByPincodeConstraint.Pincode),
                _ => throw new InvalidConstraintException(nameof(IFinderConstraint))
            };
        }
    }
}