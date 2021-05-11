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

        private readonly ILogger logger;
        private readonly CowinApiHttpClient cowinApiClient;
        private readonly IFunctionConfig fnConfig;

        public Watch(ILogger logger, CowinApiHttpClient cowinApiClient, IFunctionConfig fnConfig)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.cowinApiClient = cowinApiClient ?? throw new ArgumentNullException(nameof(cowinApiClient));
            this.fnConfig = fnConfig ?? throw new ArgumentNullException(nameof(fnConfig));
        }

        [FunctionName(STARTWATCH_HTTP)]
        public async Task Run(
            [TimerTrigger("0 */30 * * * *")] TimerInfo myTimer,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("C# {FunctionName} function executed at: {FunctionExecDate}", STARTWATCH_HTTP, DateTimeOffset.Now);

            var constraint = fnConfig.GetConstraint();
            var slotFinder = SlotFinderFactory.For(cowinApiClient, constraint);
            var result = await slotFinder.FindBy(fnConfig.GetFilter(), cancellationToken);

        }
    }
}
