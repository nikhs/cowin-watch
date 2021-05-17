using Cowin.Watch.Core.Tests.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cowin.Watch.Core.Tests.Model
{
    [TestClass]
    public class CenterResponseTests
    {
        [TestMethod]
        public void WhenResponseHasSessions_CentersResponseIsGenerated_TypeIsWithSessions()
        {
            var centersResultWithSessions = CentersEnumerable.GetFor(SampleJsonFactory.GetDefaultCentersApiResponseJson());
            Type expectedType = typeof(CentersWithSessions);

            var actualResponse = CentersResponseFactory.GetFor(centersResultWithSessions);

            Assert.IsInstanceOfType(actualResponse, expectedType);
        }

        [TestMethod]
        public void WhenResponseHasNoSession_CentersResponseIsGenerated_TypeIsWithoutSessions()
        {
            var centersResultWithoutSessions = CentersEnumerable.GetFor(SampleJsonFactory.GenerateResponseForHospitalAndVaccineWithoutSlots("a"));
            Type expectedType = typeof(CentersWithoutSessions);

            var actualResponse = CentersResponseFactory.GetFor(centersResultWithoutSessions);

            Assert.IsInstanceOfType(actualResponse, expectedType);
        }

        [TestMethod]
        public void WhenResponseHasNoData_CentersResponseIsGenerated_TypeIsNullResponse()
        {
            var emptyResult = CentersEnumerable.GetEmpty();
            Type expectedType = typeof(NullCenters);

            var actualResponse = CentersResponseFactory.GetFor(emptyResult);

            Assert.IsInstanceOfType(actualResponse, expectedType);
        }
    }
}
