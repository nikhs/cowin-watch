using Cowin.Watch.Core.Tests.Lib;
using Cowin.Watch.Core.Tests.Lib.Extensions;
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

        [TestMethod]
        public void WhenResponseHasSessions_CentersResponseIsGenerated_SessionsFoundIsTrue()
        {
            var centersResultWithSessions = CentersEnumerable.GetFor(SampleJsonFactory.GetDefaultCentersApiResponseJson());

            var actualResponse = CentersResponseFactory.GetFor(centersResultWithSessions);

            Assert.IsTrue(actualResponse.HasSessions);
        }

        [TestMethod]
        public void WhenResponseHasNoSession_CentersResponseIsGenerated_SessionsFoundIsFalse()
        {
            var centersResultWithoutSessions = CentersEnumerable.GetFor(SampleJsonFactory.GenerateResponseForHospitalAndVaccineWithoutSlots("a"));

            var actualResponse = CentersResponseFactory.GetFor(centersResultWithoutSessions);

            Assert.IsFalse(actualResponse.HasSessions);
        }

        [TestMethod]
        public void WhenResponseHasNoData_CentersResponseIsGenerated_SessionsFoundIsFalse()
        {
            var emptyResult = CentersEnumerable.GetEmpty();
            Type expectedType = typeof(NullCenters);

            var actualResponse = CentersResponseFactory.GetFor(emptyResult);

            Assert.IsFalse(actualResponse.HasSessions);
        }

        [TestMethod]
        public void WhenResponseHasSessionsWithRequiredVaccine_CentersResponseIsGenerated_HasVaccineIsTrue()
        {
            var expectedVaccine = VaccineType.Covaxin();
            var centersResultWithSessions = CentersEnumerable.GetFor(SampleJsonFactory.GenerateResponseForVaccineWithSlots(expectedVaccine));

            var actualResponse = CentersResponseFactory.GetFor(centersResultWithSessions);

            Assert.IsTrue(actualResponse.HasVaccine(expectedVaccine));
        }

        [TestMethod]
        public void WhenResponseHasSessionsWithoutRequiredVaccine_CentersResponseIsGenerated_HasVaccineIsFalse()
        {
            var vaccineUsedForResponse = VaccineType.CovidShield();
            var centersResultWithSessions = CentersEnumerable.GetFor(SampleJsonFactory.GenerateResponseForVaccineWithSlots(vaccineUsedForResponse));

            var actualResponse = CentersResponseFactory.GetFor(centersResultWithSessions);

            var expectedVaccine = VaccineType.Covaxin();
            Assert.IsFalse(actualResponse.HasVaccine(expectedVaccine));
        }

        [TestMethod]
        public void WhenResponseHasNoSession_CentersResponseIsGenerated_HasVaccineIsFalse()
        {
            var expectedVaccine = VaccineType.CovidShield();
            var centersResultWithoutSessions = CentersEnumerable.GetFor(SampleJsonFactory.GenerateResponseForVaccineWithoutSlots(expectedVaccine));

            var actualResponse = CentersResponseFactory.GetFor(centersResultWithoutSessions);

            Assert.IsFalse(actualResponse.HasVaccine(expectedVaccine));
        }

        [TestMethod]
        public void WhenResponseHasNoData_CentersResponseIsGenerated_HasVaccineIsFalse()
        {
            var emptyResult = CentersEnumerable.GetEmpty();
            Type expectedType = typeof(NullCenters);

            var actualResponse = CentersResponseFactory.GetFor(emptyResult);

            var expectedVaccine = VaccineType.CovidShield();
            Assert.IsFalse(actualResponse.HasVaccine(expectedVaccine));
        }

        [TestMethod]
        public void WhenResponseHasSessions_CentersResponseIsGenerated_ForeachIsExecuted()
        {
            var centersResultWithSessions = CentersEnumerable.GetFor(SampleJsonFactory.GetDefaultCentersApiResponseJson());

            var actualResponse = CentersResponseFactory.GetFor(centersResultWithSessions);

            Assert.That.ActionWasExecuted<Center>(actualResponse.ForEach);
        }

        [TestMethod]
        public void WhenResponseHasSessions_CentersResponseIsGenerated_ForeachIsExecuted1Time()
        {
            var centersResultWithSessions = CentersEnumerable.GetFor(SampleJsonFactory.GetDefaultCentersApiResponseJson());

            var actualResponse = CentersResponseFactory.GetFor(centersResultWithSessions);

            int expectedCount = 1;
            Assert.That.ActionWasExecutedNTime<Center>(actualResponse.ForEach, expectedCount);
        }

        [TestMethod]
        public void WhenResponseHasNoSession_CentersResponseIsGenerated_ForeachIsNotExecuted()
        {
            var centersResultWithoutSessions = CentersEnumerable.GetFor(SampleJsonFactory.GenerateResponseForHospitalAndVaccineWithoutSlots("a"));

            var actualResponse = CentersResponseFactory.GetFor(centersResultWithoutSessions);

            Assert.That.ActionWasNotExecuted<Center>(actualResponse.ForEach);
        }

        [TestMethod]
        public void WhenResponseHasNoData_CentersResponseIsGenerated_ForeachIsNotExecuted()
        {
            var emptyResult = CentersEnumerable.GetEmpty();

            var actualResponse = CentersResponseFactory.GetFor(emptyResult);

            Assert.That.ActionWasNotExecuted<Center>(actualResponse.ForEach);
        }

        [TestMethod]
        public void WhenResponseHasSessions_CentersResponseIsGenerated_NoneIsNotExecuted()
        {
            var centersResultWithSessions = CentersEnumerable.GetFor(SampleJsonFactory.GetDefaultCentersApiResponseJson());

            var actualResponse = CentersResponseFactory.GetFor(centersResultWithSessions);

            Assert.That.ActionWasNotExecuted(actualResponse.None);
        }

        [TestMethod]
        public void WhenResponseHasNoSession_CentersResponseIsGenerated_NoneIsExecuted()
        {
            var centersResultWithoutSessions = CentersEnumerable.GetFor(SampleJsonFactory.GenerateResponseForHospitalAndVaccineWithoutSlots("a"));

            var actualResponse = CentersResponseFactory.GetFor(centersResultWithoutSessions);

            Assert.That.ActionWasExecuted(actualResponse.None);
        }

        [TestMethod]
        public void WhenResponseHasNoData_CentersResponseIsGenerated_NoneIsExecuted()
        {
            var emptyResult = CentersEnumerable.GetEmpty();
            Type expectedType = typeof(NullCenters);

            var actualResponse = CentersResponseFactory.GetFor(emptyResult);

            Assert.That.ActionWasExecuted(actualResponse.None);
        }
    }
}
