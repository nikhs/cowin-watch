using Cowin.Watch.Core;
using Cowin.Watch.Function.Config;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Function
{
    public class Watch
    {
        private const string STARTWATCH_HTTP = "StartWatch_Http";

        private readonly ILogger<Watch> logger;
        private readonly CowinApiHttpClient cowinApiClient;
        private readonly IFunctionConfig fnConfig;

        public Watch(ILogger<Watch> logger, CowinApiHttpClient cowinApiClient, IFunctionConfig fnConfig)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.cowinApiClient = cowinApiClient ?? throw new ArgumentNullException(nameof(cowinApiClient));
            this.fnConfig = fnConfig ?? throw new ArgumentNullException(nameof(fnConfig));
        }

        [FunctionName(STARTWATCH_HTTP)]
        public async Task Run(
            [TimerTrigger("*/15 * * * * *")] TimerInfo myTimer,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("C# {FunctionName} function executed at: {FunctionExecDate}", STARTWATCH_HTTP, DateTimeOffset.Now);

            var constraint = fnConfig.GetConstraint();
            var slotFinder = SlotFinderFactory.For(cowinApiClient, constraint);
            var finderFilter = fnConfig.GetFilter();
            var centersFound = await slotFinder.FindBy(finderFilter, cancellationToken);

            using (logger.BeginScope<Dictionary<string, string>>(new Dictionary<string, string>() {
                { "Constraint", constraint.GetType().ToString() } ,
                { "SlotFinder", slotFinder.GetType().ToString() } ,
                { "Filter", finderFilter.GetType().ToString() }
            })) {

                logger.LogInformation("{HasSessions}", centersFound.HasSessions);
                centersFound.ForEach(c => {

                    var sessionDetails = from session in c.Sessions
                            select new {
                                CenterName = c.Name,
                                CenterLocation = c.BlockName,
                                SessionDate = session.Date,
                                SessionSlots = string.Join(",", session.Slots ?? Enumerable.Empty<string>()),
                                SessionVaccine = session.Vaccine
                            };

                    foreach (var detail in sessionDetails) {
                        logger.LogInformation("Found {Vaccine} at {CenterName} in {CenterLocation} on {SessionDate}. AvailableSlots - {Slots}",
                            detail.SessionVaccine, detail.CenterName, detail.CenterLocation, detail.SessionDate, detail.SessionSlots);
                    }
                });

                centersFound.None(() => logger.LogInformation("No slots found!"));
            }
            
        }
    }
}
