using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core.Tests.Lib.HttpClientHandler
{
    public class NoContentResponseHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var emptyResponse = new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
            return Task.FromResult(emptyResponse);
        }
    }
}