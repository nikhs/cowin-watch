using Cowin.Watch.Core.Tests.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        //[TestMethod]
        //public void When_Searching_By_Invalid_Pincode_Throws_Exception()
        //{
        //    var invalidPincode = 10^7 ;
        //    Assert.ThrowsException<ArgumentException>(() => new Pincode(invalidPincode));
        //}

        [TestMethod]
        public async Task When_Searching_By_Valid_DistrictId_And_Date_Result_Is_ValidAsync()
        {
            var validDistrictId = 56;
            var validDateTime = DateTimeOffset.Parse("2-May-2021");

            var responseJson = SampleJsonFactory.GetCentersApiResponseJson();
            var cowinApiClient = ClientFactory.GetHandlerFor_200(responseJson) as CowinApiHttpClient;
            var slotFinder = GetSlotFinderForDistrict(cowinApiClient, validDistrictId);

            var actualResult = await slotFinder.GetAvailableSlotsForDateAsync(validDateTime);
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public async Task When_Searching_By_Valid_DistrictId_And_Vaccine_Result_Is_ValidAsync()
        {
            var validDistrictId = 56;
            var validDateTime = DateTimeOffset.Parse("2-May-2021");

            var responseJson = SampleJsonFactory.GetCentersApiResponseJson();
            var cowinApiClient = ClientFactory.GetHandlerFor_200(responseJson) as CowinApiHttpClient;
            var slotFinder = GetSlotFinderForDistrict(cowinApiClient, validDistrictId);

            var expectedVaccine = VaccineType.CovidShield();
            var actualResult = await slotFinder.GetAvailableSlotsForDateAndVaccineAsync(validDateTime, expectedVaccine);
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public async Task When_Searching_By_Valid_DistrictId_And_Vaccine_Result_Contains_Only_Valid_Vaccines()
        {
            var validDistrictId = 56;
            var validDateTime = DateTimeOffset.Parse("2-May-2021");

            var responseJson = SampleJsonFactory.GetCentersApiResponseJson();
            var cowinApiClient = ClientFactory.GetHandlerFor_200(responseJson) as CowinApiHttpClient;
            var slotFinder = GetSlotFinderForDistrict(cowinApiClient, validDistrictId);

            var expectedVaccine = VaccineType.CovidShield();
            var actualResult = await slotFinder.GetAvailableSlotsForDateAndVaccineAsync(validDateTime, expectedVaccine);
            Assert.IsTrue(ResultHasExpectedVaccine(actualResult, expectedVaccine));
        }

        private bool ResultHasExpectedVaccine(IEnumerable<Center> actualResult, VaccineType expectedVaccine)
        {
            return actualResult
                .Any(c => c.Sessions
                .All(s => expectedVaccine.Equals(s.Vaccine)));
        }

        private SlotFinderByDistrictId GetSlotFinderForDistrict(CowinApiHttpClient cowinApiClient, int districtId)
        {
            return new SlotFinderByDistrictId(cowinApiClient, DistrictId.FromInt(districtId));
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
