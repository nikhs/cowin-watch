using Cowin.Watch.Core.Tests.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cowin.Watch.Core.Tests
{
    [TestClass]
    class SlotFinderTests
    {
        [TestMethod]
        public void When_Searching_By_Invalid_Districtid_Throws_Exception()
        {
            var invalidDistrictId = -1;
            Assert.ThrowsException<ArgumentException>(() => DistrictId.FromLong(invalidDistrictId));
        }

        //[TestMethod]
        //public void When_Searching_By_Invalid_Pincode_Throws_Exception()
        //{
        //    var invalidPincode = 10^7 ;
        //    Assert.ThrowsException<ArgumentException>(() => new Pincode(invalidPincode));
        //}

        [TestMethod]
        public void When_Searching_By_Valid_DistrictId_And_Date_Result_Is_Valid()
        {
            var validDistrictId = 56;
            var validDateTime = DateTimeOffset.Parse("2-May-2021");
            var cowinApiClient = ClientFactory.GetHandlerFor_204() as CowinApiHttpClient;
            var slotFinder = GetSlotFinderForDistrict(cowinApiClient, DistrictId.FromLong(validDistrictId));

            Root actualResult = slotFinder.FindForDate(validDateTime);
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public void When_Searching_By_Valid_DistrictId_And_Vaccine_Result_Is_Valid()
        {
            var validDistrictId = 56;
            var validDateTime = DateTimeOffset.Parse("2-May-2021");

            var cowinApiClient = ClientFactory.GetHandlerFor_204() as CowinApiHttpClient;
            var slotFinder = GetSlotFinderForDistrict(cowinApiClient, DistrictId.FromLong(validDistrictId));

            var expectedVaccine = VaccineType.CovidShield();
            Root actualResult = slotFinder.FindForDateAndVaccine(validDateTime, expectedVaccine);
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public void When_Searching_By_Valid_DistrictId_And_Vaccine_Result_Contains_Only_Valid_Vaccines()
        {
            var validDistrictId = 56;
            var validDateTime = DateTimeOffset.Parse("2-May-2021");

            var cowinApiClient = ClientFactory.GetHandlerFor_204() as CowinApiHttpClient;
            var slotFinder = GetSlotFinderForDistrict(cowinApiClient, DistrictId.FromLong(validDistrictId));

            var expectedVaccine = VaccineType.CovidShield();
            Root actualResult = slotFinder.FindForDateAndVaccine(validDateTime, expectedVaccine);
            Assert.IsTrue(ResultHasExpectedVaccine(actualResult, expectedVaccine));
        }

        private bool ResultHasExpectedVaccine(Root actualResult, object expectedVaccine)
        {
            return actualResult.Centers
                .Any(c => c.Sessions
                .Any(s => !VaccineType.IsCovidShield(s.Vaccine)));
        }

        private SlotFinderByDistrictId GetSlotFinderForDistrict(CowinApiHttpClient cowinApiClient, DistrictId districtId)
        {
            return new SlotFinderByDistrictId(cowinApiClient, districtId);
        }
    }
}
