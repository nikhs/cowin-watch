using Cowin.Watch.Core.Tests.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core.Tests
{
    [TestClass]
    public class SlotFinderByFilterTests
    {
        [TestMethod]
        public async Task When_Searching_By_Valid_DistrictId_And_Date_Result_Is_ValidAsync()
        {
            var district = DistrictId.FromInt(56);
            var from = DateTimeOffset.Parse("2-May-2021");

            var cowinApiClient = ClientFactory.GetDefaultHandlerFor_200() as CowinApiHttpClient;
            ISlotFinder slotFinder = SlotFinderFactory.For(cowinApiClient, FinderConstraintFactory.From(district));
            IFinderFilter dateFromFilter = FinderFilterFactory.From(from);

            var actualResult = await slotFinder.FindBy(dateFromFilter, CancellationToken.None);
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public async Task When_Searching_By_Valid_Pincode_And_Date_Result_Is_ValidAsync()
        {
            var pincode  = Pincode.FromString("673529");
            var from = DateTimeOffset.Parse("2-May-2021");

            var cowinApiClient = ClientFactory.GetDefaultHandlerFor_200() as CowinApiHttpClient;
            ISlotFinder slotFinder = SlotFinderFactory.For(cowinApiClient, FinderConstraintFactory.From(pincode));
            IFinderFilter dateFromFilter = FinderFilterFactory.From(from);

            var actualResult = await slotFinder.FindBy(dateFromFilter, CancellationToken.None);
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public async Task WhenSearchingByDistrict_ResultHasSessions_TypeIsCentersWithSession()
        {
            var district = DistrictId.FromInt(56);
            var from = DateTimeOffset.Parse("2-May-2021");
            var cowinApiClient = ClientFactory.GetDefaultHandlerFor_200() as CowinApiHttpClient;
            ISlotFinder slotFinder = SlotFinderFactory.For(cowinApiClient, FinderConstraintFactory.From(district));
            IFinderFilter dateFromFilter = FinderFilterFactory.From(from);
            Type expectedType = typeof(CentersWithSessions);

            var actualResult = await slotFinder.FindBy(dateFromFilter, CancellationToken.None);

            Assert.IsInstanceOfType(actualResult, expectedType);
        }


        [TestMethod]
        public async Task WhenSearchingByDistrict_ResultHasNoSessions_TypeIsCentersWithoutSession()
        {
            var district = DistrictId.FromInt(56);
            var from = DateTimeOffset.Parse("2-May-2021");
            string responseWithoutSlots = SampleJsonFactory.GenerateResponseForHospitalAndVaccineWithoutSlots("Hosp");
            var cowinApiClient = ClientFactory.GetHandlerFor_200(responseWithoutSlots) as CowinApiHttpClient;
            ISlotFinder slotFinder = SlotFinderFactory.For(cowinApiClient, FinderConstraintFactory.From(district));
            IFinderFilter dateFromFilter = FinderFilterFactory.From(from);
            Type expectedType = typeof(CentersWithoutSessions);

            var actualResult = await slotFinder.FindBy(dateFromFilter, CancellationToken.None);

            Assert.IsInstanceOfType(actualResult, expectedType);
        }


        [TestMethod]
        public async Task WhenSearchingByDistrict_ResultIsEmpty_TypeIsNullCenters()
        {
            var district = DistrictId.FromInt(56);
            var from = DateTimeOffset.Parse("2-May-2021");
            var cowinApiClient = ClientFactory.GetHandlerFor_204() as CowinApiHttpClient;
            ISlotFinder slotFinder = SlotFinderFactory.For(cowinApiClient, FinderConstraintFactory.From(district));
            IFinderFilter dateFromFilter = FinderFilterFactory.From(from);
            Type expectedType = typeof(NullCenters);

            var actualResult = await slotFinder.FindBy(dateFromFilter, CancellationToken.None);

            Assert.IsInstanceOfType(actualResult, expectedType);
        }
    }
}
