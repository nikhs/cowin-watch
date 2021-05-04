using Cowin.Watch.Core.ApiClient;
using Cowin.Watch.Core.Tests.Lib;
using Cowin.Watch.Core.Tests.Lib.HttpClientHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core.Tests
{
    [TestClass]
    public class QueryTests
    {

        [TestMethod]
        public void Does_Client_Handle_204()
        {
            var districtId = 15; var dateFrom = DateTimeOffset.Parse("04-May-2021");
            var cowinApiClient = ClientFactory.GetHandlerFor_204();

            Assert.ThrowsExceptionAsync<NullReferenceException>(async () => await cowinApiClient.GetSessionsForDistrictAndDateAsync(districtId, dateFrom, CancellationToken.None));
        }

        [TestMethod]
        public async Task Does_Client_Parse_Valid_Content()
        {
            var districtId = 15; var dateFrom = DateTimeOffset.Parse("04-May-2021");
            string content = SampleJsonFactory.GetCentersApiResponseJson();
            var cowinApiClient = ClientFactory.GetHandlerFor_200(content);

            Root actualResponse = await cowinApiClient.GetSessionsForDistrictAndDateAsync(districtId, dateFrom, CancellationToken.None);
            Assert.IsNotNull(actualResponse);
        }

        [TestMethod]
        public void When_Request_Is_Cancelled_An_AggregateException_Is_Thrown()
        {
            var districtId = 15; var dateFrom = DateTimeOffset.Parse("04-May-2021");
            var cowinApiClient = ClientFactory.GetHandlerFor_DelayedResponse();
            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(2));

            Assert.ThrowsExceptionAsync<AggregateException>(async () =>
                await cowinApiClient.GetSessionsForDistrictAndDateAsync(districtId, dateFrom, cancellationTokenSource.Token));
        }
    }

    public static class ClientFactory
    {
        public static ICowinApiClient GetCowinClientForHandler(DelegatingHandler delegatingHandler) =>
            new CowinApiHttpClient(GetDefaultHttpClient(delegatingHandler));

        public static ICowinApiClient GetHandlerFor_204() =>
            new CowinApiHttpClient(GetDefaultHttpClient(new NoContentResponseHandler()));

        public static ICowinApiClient GetHandlerFor_200<TContent>(TContent content) =>
            new CowinApiHttpClient(GetDefaultHttpClient(OkResponseHandler<TContent>.ForContent<TContent>(content)));

        public static ICowinApiClient GetHandlerFor_DelayedResponse() =>
            new CowinApiHttpClient(GetDefaultHttpClient(DelayedResponseHandler.Instance));

        public static HttpClient GetDefaultHttpClient(HttpMessageHandler httpMessageHandler) =>
            new HttpClient(httpMessageHandler)
            {
                BaseAddress = new Uri("http://localhost/")
            };

    }
}
