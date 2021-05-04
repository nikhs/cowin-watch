using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Cowin.Watch.Function
{
    public static class Watch
    {
        [FunctionName("StartWatch_Http")]
        public static void Run([TimerTrigger("0 */30 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

        }
    }
}
