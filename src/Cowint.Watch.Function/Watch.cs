using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cowin.Watch.Core;
using Cowin.Watch.Function.Config;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Cowin.Watch.Function
{
    public static class Watch
    {
        private const string STARTWATCH_HTTP = "StartWatch_Http";

        [FunctionName(STARTWATCH_HTTP)]
        public static async Task Run(
            [TimerTrigger("0 */30 * * * *")] TimerInfo myTimer,
            ILogger log,
            SlotFinderByDistrictId SlotFinderByDistrictId)
        {
            log.LogInformation($"C# {STARTWATCH_HTTP} function executed at: {DateTime.Now}");
            DateTime dateToQuery = DateTime.Now;

            if (EnvVariables.SearchByVaccine() == null) {
                log.LogInformation($"Finding slots in district {SlotFinderByDistrictId.DistrictId} for {dateToQuery:d}");
                var result =  await SlotFinderByDistrictId.GetAvailableSlotsForDateAsync(dateToQuery);

                if  ( result.Any()) {
                    LogResults(result, log, SlotFinderByDistrictId.DistrictId);
                }
                else {
                    LogNone(log, SlotFinderByDistrictId.DistrictId);
                }
                return;
            }

            VaccineType vaccineToSearch = EnvVariables.SearchByVaccine();
            log.LogInformation($"Finding slots for {vaccineToSearch} in district {SlotFinderByDistrictId.DistrictId} for {dateToQuery:d}");
            var resultForVaccine = await SlotFinderByDistrictId.GetAvailableSlotsForDateAndVaccineAsync(dateToQuery, vaccineToSearch);

            if (resultForVaccine.Any()) {
                LogResults(resultForVaccine, log, SlotFinderByDistrictId.DistrictId, vaccineToSearch);
            }
            else {
                LogNone(log, SlotFinderByDistrictId.DistrictId, vaccineToSearch);
            }
        }

        private static void LogNone(ILogger logger, DistrictId districtId, VaccineType vaccineToSearch)
        {
            logger.LogInformation($"No slots for {vaccineToSearch} for districtId={districtId.Value}");
        }

        private static void LogResults(IEnumerable<Center> centers, ILogger logger, DistrictId districtId, VaccineType vaccineToSearch)
        {
            foreach (var center in centers) {
                foreach (var session in center.Sessions) {
                    logger.LogInformation($"Found slots for {session.Vaccine} for districtId={districtId.Value} at {center.Name} on {session.Date:d}");
                }
            }
        }

        private static void LogNone(ILogger logger, DistrictId districtId)
        {
            logger.LogInformation($"No slots for districtId={districtId.Value}");
        }

        private static void LogResults(IEnumerable<Center> centers, ILogger logger, DistrictId districtId)
        {
            foreach(var center in centers) {
                foreach(var session in center.Sessions) {
                    logger.LogInformation($"Found slots for {session.Vaccine} for districtId={districtId.Value} at {center.Name} on {session.Date:d}");
                }
            }
        }
    }
}
