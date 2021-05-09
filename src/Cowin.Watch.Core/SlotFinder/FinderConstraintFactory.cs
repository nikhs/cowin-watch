﻿using System;

namespace Cowin.Watch.Core
{
    public class FinderConstraintFactory
    {
        public static IFinderConstraint From(DistrictId districtId) => new SearchByDistrictConstraint(districtId);
        public static IFinderConstraint From(Pincode pincode) => new SearchByPincodeConstraint(pincode);
    }
}