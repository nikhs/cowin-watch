using Cowin.Watch.Core.Tests.Lib;
using Cowin.Watch.Function.Config;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cowin.Watch.Core.Tests
{
    [TestClass]
    class WatchFunctionTests
    {
        [TestMethod]
        public async Task When_WatchFunction_Starts_Logging_Works()
        {
            var districtId = DistrictId.FromInt(56);
            var logger = GetLogger();
            string content = SampleJsonFactory.GetCentersApiResponseJson();
            CowinApiHttpClient cowinApiClient = GetCowinApiClient(content);
            var slotFinderByDistrictId = new SlotFinderByDistrictId(cowinApiClient, districtId);

            await Cowin.Watch.Function.Watch.Run(null, logger, slotFinderByDistrictId);
            var logs = (logger as ListLogger).Logs;

            Assert.IsNotNull(logger);
            Assert.IsTrue(logs.Count > 0);
            Assert.IsTrue(logs[1].Contains("Finding"));
        }

        private static CowinApiHttpClient GetCowinApiClient(string content)
        {
            return ClientFactory.GetHandlerFor_200(content) as CowinApiHttpClient;
        }

        [TestMethod]
        public async Task When_WatchFunction_Starts_If_Api_Has_Slots_Result_Is_Logged()
        {
            var hospital = "Hosp";
            var vaccine = VaccineType.Covaxin();
            var vaccineCount = 10;
            var sessionDate = DateTimeOffset.Now;
            var districtId = DistrictId.FromInt(56);

            var logger = GetLogger();
            string content = SampleJsonFactory.GenerateResponseWithSessions(hospital, vaccine, vaccineCount, sessionDate);
            CowinApiHttpClient cowinApiClient = GetCowinApiClient(content);
            var slotFinderByDistrictId = new SlotFinderByDistrictId(cowinApiClient, districtId);

            await Cowin.Watch.Function.Watch.Run(null, logger, slotFinderByDistrictId);
            var logs = (logger as ListLogger).Logs;

            var expectedLog = GetExpectedSuccessLog(hospital, vaccine, sessionDate, districtId);

            Assert.IsTrue(logs.Any(line =>
                line.Contains(expectedLog)));
        }

        [TestMethod]
        public async Task When_WatchFunction_Starts_If_Api_Has_No_Slots_Result_Is_Logged()
        {
            var hospital = "Hosp";
            var vaccine = VaccineType.Covaxin();
            var districtId = DistrictId.FromInt(56);

            var logger = GetLogger();
            string content = SampleJsonFactory.GenerateResponseForHospitalAndVaccineWithoutSlots(hospital, vaccine);
            CowinApiHttpClient cowinApiClient = GetCowinApiClient(content);
            var slotFinderByDistrictId = new SlotFinderByDistrictId(cowinApiClient, districtId);

            await Cowin.Watch.Function.Watch.Run(null, logger, slotFinderByDistrictId);
            var logs = (logger as ListLogger).Logs;

            var expectedLog = GetExpectedFailLog(hospital, vaccine, districtId);

            Assert.IsTrue(logs.Any(line =>
                line.Contains(expectedLog)));
        }

        [TestMethod]
        public async Task When_WatchFunction_Starts_If_Api_Has_Slots_For_Vaccine_Result_Is_Logged()
        {
            var expectedVaccine = VaccineType.Covaxin();

            var hospital = "Hosp";
            var vaccineCount = 10;
            var sessionDate = DateTimeOffset.Now;
            var districtId = DistrictId.FromInt(56);

            var logger = GetLogger();
            string content = SampleJsonFactory.GenerateResponseWithSessions(hospital, expectedVaccine, vaccineCount, sessionDate);
            CowinApiHttpClient cowinApiClient = GetCowinApiClient(content);
            var slotFinderByDistrictId = new SlotFinderByDistrictId(cowinApiClient, districtId);

            Environment.SetEnvironmentVariable(EnvVariables.KEY_SearchByVaccine, expectedVaccine.ToString());
            await Cowin.Watch.Function.Watch.Run(null, logger, slotFinderByDistrictId);
            var logs = (logger as ListLogger).Logs;

            var expectedLog = GetExpectedSuccessLog(hospital, expectedVaccine, sessionDate, districtId);

            Assert.IsTrue(logs.Any(line =>
                line.Contains(expectedLog)));
        }

        [TestMethod]
        public async Task When_WatchFunction_Starts_If_Api_Has_No_Slots_For_Vaccine_Result_Is_Logged()
        {
            var expectedVaccine = VaccineType.Covaxin();

            var hospital = "Hosp";
            var districtId = DistrictId.FromInt(56);

            var logger = GetLogger();
            string content = SampleJsonFactory.GenerateResponseForHospitalAndVaccineWithoutSlots(hospital, expectedVaccine);
            CowinApiHttpClient cowinApiClient = GetCowinApiClient(content);
            var slotFinderByDistrictId = new SlotFinderByDistrictId(cowinApiClient, districtId);

            Environment.SetEnvironmentVariable(EnvVariables.KEY_SearchByVaccine, expectedVaccine.ToString());
            await Cowin.Watch.Function.Watch.Run(null, logger, slotFinderByDistrictId);
            var logs = (logger as ListLogger).Logs;

            var expectedLog = GetExpectedFailLog(hospital, expectedVaccine, districtId);

            Assert.IsTrue(logs.Any(line =>
                line.Contains(expectedLog)));
        }

        private string GetExpectedFailLog(string hospital, VaccineType vaccine, DistrictId districtId)
        {
            return $"No slots for {vaccine} for districtId={districtId}";
        }

        private static string GetExpectedSuccessLog(string hospital, VaccineType vaccine, DateTimeOffset sessionDate, DistrictId districtId)
        {
            return $"Found slots for {vaccine} for districtId={districtId} at {hospital} on {sessionDate:d}";
        }

        private ILogger GetLogger() => new ListLogger();
    }

}
