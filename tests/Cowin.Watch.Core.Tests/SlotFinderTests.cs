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
        public async Task When_Searching_By_Valid_Pincode_And_Date_Result_Is_ValidAsync()
        {
            var pincode = "123456";
            var dateFrom = DateTimeOffset.Parse("2-May-2021");
            var slotFinder = GetSlotFinderForPincode_DefaultResponse(pincode);

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
        public async Task When_Searching_By_Valid_Pincode_And_Vaccine_Result_Is_ValidAsync()
        {
            var pincode = "123456";
            var dateFrom = DateTimeOffset.Parse("2-May-2021");
            var expectedVaccine = VaccineType.CovidShield();
            var slotFinder = GetSlotFinderForPincode_DefaultResponse(pincode);

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

        [TestMethod]
        public async Task When_Searching_By_Valid_Pincode_And_Vaccine_Result_Contains_Only_Valid_Vaccines()
        {
            var pincode = "123456";
            var cowinApiClient = ClientFactory.GetDefaultHandlerFor_200() as CowinApiHttpClient;
            var slotFinder = GetSlotFinderForPincode(cowinApiClient, pincode);

            var dateFrom = DateTimeOffset.Parse("2-May-2021");
            var expectedVaccine = VaccineType.CovidShield();

            var actualResult = await slotFinder.FindBy(FinderFilterFactory.From(dateFrom, expectedVaccine), CancellationToken.None);

            Assert.IsTrue(ResultHasExpectedVaccine(actualResult, expectedVaccine));
        }

        [TestMethod]
        public void When_COVISHIELD_Is_Compared_To_VaccineType_Returns_True()
        {
            var expectedVaccine = VaccineType.CovidShield();
            var actualString = "COVISHIELD";
            Assert.IsTrue(expectedVaccine.Equals(actualString));
        }

        private bool ResultHasExpectedVaccine(IEnumerable<Center> actualResult, VaccineType expectedVaccine)
        {
            return actualResult
                .Any(c => c.Sessions
                .All(s => expectedVaccine.Equals(s.Vaccine)));
        }

        private bool ResultHasExpectedVaccine(ICentersResponse actualResult, VaccineType expectedVaccine)
        {
            return actualResult
                .HasVaccine(expectedVaccine);
        }

        private ISlotFinder GetSlotFinderForDistrict_DefaultResponse(int districtId)
        {
            var cowinApiClient = ClientFactory.GetDefaultHandlerFor_200() as CowinApiHttpClient;
            return GetSlotFinderForDistrict(cowinApiClient, districtId);
        }

        private ISlotFinder GetSlotFinderForPincode_DefaultResponse(string pincode)
        {
            var cowinApiClient = ClientFactory.GetDefaultHandlerFor_200() as CowinApiHttpClient;
            return GetSlotFinderForPincode(cowinApiClient, pincode);
        }

        private ISlotFinder GetSlotFinderForDistrict(CowinApiHttpClient cowinApiClient, int districtId) =>
            SlotFinderFactory.For(cowinApiClient, FinderConstraintFactory.From(DistrictId.FromInt(districtId)));

        private ISlotFinder GetSlotFinderForPincode(CowinApiHttpClient cowinApiClient, string pincode) =>
            SlotFinderFactory.For(cowinApiClient, FinderConstraintFactory.From(Pincode.FromString(pincode)));
    }
}
