using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core.Tests
{
    internal class DelayedResponseHandler : HttpMessageHandler
    {
        internal static readonly HttpMessageHandler Instance = new DelayedResponseHandler(waitTimeInSeconds: 10);
        private readonly int waitTimeInSeconds;

        public DelayedResponseHandler(int waitTimeInSeconds)
        {
            this.waitTimeInSeconds = waitTimeInSeconds;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            Thread.Sleep(waitTimeInSeconds * 1000);
            return Task.FromResult(responseMessage);
        }
    }
}