using Cowin.Watch.Core.ApiClient;
using Cowin.Watch.Core.Tests.Lib.HttpClientHandler;
using System;
using System.Net.Http;

namespace Cowin.Watch.Core.Tests.Lib
{
    public static class ClientFactory
    {
        public static ICowinApiClient GetCowinClientForHandler(DelegatingHandler delegatingHandler) =>
            new CowinApiHttpClient(GetDefaultHttpClient(delegatingHandler));

        public static ICowinApiClient GetHandlerFor_204() =>
            new CowinApiHttpClient(GetDefaultHttpClient(new NoContentResponseHandler()));

        public static ICowinApiClient GetHandlerFor_200<TContent>(TContent content) =>
            new CowinApiHttpClient(GetDefaultHttpClient(OkResponseHandler<TContent>.ForContent<TContent>(content)));

        public static ICowinApiClient GetDefaultHandlerFor_200() =>
    new CowinApiHttpClient(GetDefaultHttpClient(OkResponseHandler<string>.ForContent<string>(SampleJsonFactory.GetDefaultCentersApiResponseJson())));


        public static ICowinApiClient GetHandlerFor_DelayedResponse() =>
            new CowinApiHttpClient(GetDefaultHttpClient(DelayedResponseHandler.Instance));

        public static HttpClient GetDefaultHttpClient(HttpMessageHandler httpMessageHandler) =>
            new HttpClient(httpMessageHandler)
            {
                BaseAddress = new Uri("http://localhost/")
            };

    }
}
