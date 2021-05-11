namespace Cowin.Watch.Core
{
    public interface IFinderConstraint
    {
    }

    internal class SearchByDistrictConstraint: IFinderConstraint
    {
        public DistrictId DistrictId { get; }

        public SearchByDistrictConstraint(DistrictId districtId)
        {
            DistrictId = districtId ?? throw new System.ArgumentNullException(nameof(districtId));
        }
    }

    internal class SearchByPincodeConstraint : IFinderConstraint
    {
        public Pincode Pincode { get; }

        public SearchByPincodeConstraint(Pincode pincode)
        {
            Pincode = pincode ?? throw new System.ArgumentNullException(nameof(pincode));
        }
    }
}