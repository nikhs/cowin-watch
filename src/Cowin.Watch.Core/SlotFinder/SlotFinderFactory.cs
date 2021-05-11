using Cowin.Watch.Core.SlotFinder;
using System;

namespace Cowin.Watch.Core
{
    public static class SlotFinderFactory
    {
        public static ISlotFinder For(CowinApiHttpClient cowinApiClient, IFinderConstraint finderConstraint)
        {
            return finderConstraint switch {
                SearchByDistrictConstraint searchByDistrictConstraint => SlotFinderByDistrictId.From(cowinApiClient, searchByDistrictConstraint),
                SearchByPincodeConstraint searchByPincodeConstraint => SlotFinderByPincode.From(cowinApiClient, searchByPincodeConstraint),
                _ => throw new InvalidConstraintException($"{finderConstraint.GetType().Name} is not a valid constraint type!")
            };
        }
    }
}