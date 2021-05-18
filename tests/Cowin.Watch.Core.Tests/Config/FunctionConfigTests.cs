using Cowin.Watch.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cowin.Watch.Function
{
    [TestClass]
    public class FunctionConfigTests
    {
        [DataTestMethod()]
        [DataRow("1", "")]
        [DataRow("1", "123456")]
        public void FunctionConstraintFactory_WhenOnlyDistrictIdIsProvided_ConstraintIsSearchByDistrict(string districtId, string pincode)
        {
            var config = FunctionConfigFactory.FromMemory(districtId: districtId, pincode: pincode, vaccine: string.Empty);

            var constraint = config.GetConstraint();

            Assert.IsInstanceOfType(constraint, typeof(SearchByDistrictConstraint));
        }

        [DataTestMethod()]
        [DataRow("1", "")]
        [DataRow("1", "123456")]
        public void FunctionConstraintFactory_WhenOnlyDistrictIdIsProvided_SearchByDistrictConstraintHasDistrictId(string districtId, string pincode)
        {
            var config = FunctionConfigFactory.FromMemory(districtId: districtId, pincode: pincode, vaccine: string.Empty);

            var constraint = config.GetConstraint();

            Assert.AreEqual(districtId, (constraint as SearchByDistrictConstraint).DistrictId.ToString());
        }

        [TestMethod]
        public void FunctionConstraintFactory_WhenNoDistrictIdIsProvided_ConstraintIsSearchByPincode()
        {
            const string pincode = "123456";
            var config = FunctionConfigFactory.FromMemory(districtId: string.Empty, pincode: pincode, vaccine: string.Empty);

            var constraint = config.GetConstraint();

            Assert.IsInstanceOfType(constraint, typeof(SearchByPincodeConstraint));
        }

        [TestMethod]
        public void FunctionConstraintFactory_WhenNoDistrictIdIsProvided_IsSearchByPincodeHasPincode()
        {
            const string pincode = "123456";
            var config = FunctionConfigFactory.FromMemory(districtId: string.Empty, pincode: pincode, vaccine: string.Empty);

            var constraint = config.GetConstraint();

            Assert.AreEqual(pincode, (constraint as SearchByPincodeConstraint).Pincode.ToString());
        }

        [TestMethod]
        public void FunctionConstraintFactory_WhenNoVaccineIsProvided_FilterIsOnlyDate()
        {
            var config = FunctionConfigFactory.FromMemory(districtId: string.Empty, pincode: string.Empty, vaccine: string.Empty);

            var filter = config.GetFilter();

            Assert.IsInstanceOfType(filter, typeof(DateOnlyFinder));
        }

        [TestMethod]
        public void FunctionConstraintFactory_WhenVaccineIsProvided_FilterIsForVaccineAndDate()
        {
            var config = FunctionConfigFactory.FromMemory(districtId: string.Empty, pincode: string.Empty, vaccine: "covishield");

            var filter = config.GetFilter();

            Assert.IsInstanceOfType(filter, typeof(VaccineFinder));
        }
    }
}