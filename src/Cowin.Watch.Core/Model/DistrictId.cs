using System;

namespace Cowin.Watch.Core
{
    public class DistrictId
    {
        private readonly int districtId;

        protected DistrictId(int districtId)
        {
            this.districtId = districtId;
        }

        private static void ValidateDistrictId(int districtId)
        {
            if (districtId < 1) {
                throw new ArgumentException($"{nameof(districtId)} should be above 0");
            }
        }

        public static DistrictId FromInt(int districtId)
        {
            ValidateDistrictId(districtId);
            return new DistrictId(districtId);
        }

        public override string ToString()
        {
            return districtId.ToString();
        }
    }
}