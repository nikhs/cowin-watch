using Cowin.Watch.Core.Tests.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core.Tests
{
    [TestClass]
    public class SlotFinderTests
    {
        [TestMethod]
        public void When_Searching_By_Invalid_Districtid_Throws_Exception()
        {
            var invalidDistrictId = -1;
            Assert.ThrowsException<ArgumentException>(() => DistrictId.FromInt(invalidDistrictId));
        }

        [TestMethod]
        public void When_Searching_By_Invalid_Pincode_Throws_Exception()
        {
            var invalidPincode = "7777777";
            Assert.ThrowsException<ArgumentException>(() => Pincode.FromString(invalidPincode));
        }

        [TestMethod]
        public async Task When_Searching_By_Valid_DistrictId_And_Date_Result_Is_ValidAsync()
        {
            var district = 56;
            var dateFrom = DateTimeOffset.Parse("2-May-2021");

            var slotFinder = GetSlotFinderForDistrict_DefaultResponse(district);

            var actualResult = await slotFinder.FindBy(FinderFilterFactory.From(dateFrom), CancellationToken.None);
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public async Task When_Searching_By_Valid_DistrictId_And_Vaccine_Result_Is_ValidAsync()
        {
            var district = 56;
            var dateFrom = DateTimeOffset.Parse("2-May-2021");
            var expectedVaccine = VaccineType.CovidShield();

            var slotFinder = GetSlotFinderForDistrict_DefaultResponse(district);

            var actualResult = await slotFinder.FindBy(FinderFilterFactory.From(dateFrom, expectedVaccine), CancellationToken.None);

            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public async Task When_Searching_By_Valid_DistrictId_And_Vaccine_Result_Contains_Only_Valid_Vaccines()
        {
            var district = 56;
            var cowinApiClient = ClientFactory.GetDefaultHandlerFor_200() as CowinApiHttpClient;
            var slotFinder = GetSlotFinderForDistrict(cowinApiClient, district);

            var dateFrom = DateTimeOffset.Parse("2-May-2021");
            var expectedVaccine = VaccineType.CovidShield();

            var actualResult = await slotFinder.FindBy(FinderFilterFactory.From(dateFrom, expectedVaccine), CancellationToken.None);

            Assert.IsTrue(ResultHasExpectedVaccine(actualResult, expectedVaccine));
        }

        private bool ResultHasExpectedVaccine(IEnumerable<Center> actualResult, VaccineType expectedVaccine)
        {
            return actualResult
                .Any(c => c.Sessions
                .All(s => expectedVaccine.Equals(s.Vaccine)));
        }

        private ISlotFinder GetSlotFinderForDistrict_DefaultResponse(int districtId)
        {
            var responseJson = SampleJsonFactory.GetDefaultCentersApiResponseJson();
            var cowinApiClient = ClientFactory.GetHandlerFor_200(responseJson) as CowinApiHttpClient;

            DistrictId district = DistrictId.FromInt(districtId);
            return SlotFinderFactory.For(cowinApiClient, FinderConstraintFactory.From(district));
        }

        private ISlotFinder GetSlotFinderForDistrict(CowinApiHttpClient cowinApiClient, int districtId)
        {
            DistrictId district = DistrictId.FromInt(districtId);
            return SlotFinderFactory.For(cowinApiClient, FinderConstraintFactory.From(district));
        }

        [TestMethod]
        public void When_COVISHIELD_Is_Compared_To_VaccineType_Returns_True()
        {
            var expectedVaccine = VaccineType.CovidShield();
            var actualString = "COVISHIELD";
            Assert.IsTrue(expectedVaccine.Equals(actualString));
        }
    }
}
