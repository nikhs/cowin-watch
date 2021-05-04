using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core.Tests.Lib.HttpClientHandler
{
    public class OkResponseHandler<TContent> : HttpMessageHandler
    {
        private readonly TContent content;

        public OkResponseHandler(TContent content)
        {
            this.content = content;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            responseMessage.Content = content switch
            {
                string stringContent => new StringContent(stringContent, new UTF8Encoding(), "application/json"),
                _ => JsonContent.Create<TContent>(content),
            };
            return Task.FromResult(responseMessage);
        }

        public static OkResponseHandler<T> ForContent<T>(T content) =>
            new OkResponseHandler<T>(content);

    }
}
