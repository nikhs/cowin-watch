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
            var validDistrictId = 56;
            var validDateTime = DateTimeOffset.Parse("2-May-2021");

            var responseJson = SampleJsonFactory.GetCentersApiResponseJson();
            var cowinApiClient = ClientFactory.GetHandlerFor_200(responseJson) as CowinApiHttpClient;
            
            IFinderConstraint finderConstraint = FinderConstraintFactory.From(validDistrictId);
            ISlotFinder slotFinder = SlotFinderFactory.For(cowinApiClient, finderConstraint);
            IFinderFilter finderFilter = FinderFilterFactory.From(validDateTime);

            var actualResult = await slotFinder.FindBy(finderFilter, CancellationToken.None);
            Assert.IsNotNull(actualResult);
        }

        
    }
}
