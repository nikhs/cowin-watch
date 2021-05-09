using Cowin.Watch.Core.ApiClient;
using Cowin.Watch.Core.Tests.Lib.HttpClientHandler;
using System;
using System.Net.Http;

namespace Cowin.Watch.Core.Tests.Lib
{
    public static class ClientFactory
    {
        public static ICowinApiClient GetCowinClientForHandler(DelegatingHandler delegatingHandler) =>
            new CowinApiHttpClient(GetDefaultHttpClient(delegatingHandler), new ListLogger());

        public static ICowinApiClient GetHandlerFor_204() =>
            new CowinApiHttpClient(GetDefaultHttpClient(new NoContentResponseHandler()), new ListLogger());

        public static ICowinApiClient GetHandlerFor_200<TContent>(TContent content) =>
            new CowinApiHttpClient(GetDefaultHttpClient(OkResponseHandler<TContent>.ForContent<TContent>(content)), new ListLogger());

        public static ICowinApiClient GetDefaultHandlerFor_200()
        {
            string content = SampleJsonFactory.GetDefaultCentersApiResponseJson();
            return new CowinApiHttpClient(GetDefaultHttpClient(OkResponseHandler<string>.ForContent<string>(content)), new ListLogger());
        }

        public static ICowinApiClient GetHandlerFor_DelayedResponse() =>
            new CowinApiHttpClient(GetDefaultHttpClient(DelayedResponseHandler.Instance), new ListLogger());

        public static HttpClient GetDefaultHttpClient(HttpMessageHandler httpMessageHandler) =>
            new HttpClient(httpMessageHandler)
            {
                BaseAddress = new Uri("http://localhost/")
            };

    }
}
