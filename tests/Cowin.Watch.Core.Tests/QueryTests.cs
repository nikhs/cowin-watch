using Cowin.Watch.Core.ApiClient;
using Cowin.Watch.Core.Tests.Lib;
using Cowin.Watch.Core.Tests.Lib.HttpClientHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
            
            Assert.ThrowsExceptionAsync<NullReferenceException>(async () => await cowinApiClient.GetSessionsForDistrictAndDateAsync(districtId, dateFrom));
        }

        [TestMethod]
        public async Task Does_Client_Handle_Valid_Content()
        {
            var districtId = 15; var dateFrom = DateTimeOffset.Parse("04-May-2021");
            string content = SampleJsonFactory.GetCentersApiResponseJson();
            var cowinApiClient = ClientFactory.GetHandlerFor_200(content);
            
            Root expectedResponse = JsonSerializer.Deserialize<Root>(content);
            Root actualResponse = await cowinApiClient.GetSessionsForDistrictAndDateAsync(districtId, dateFrom);

            Assert.AreEqual<Root>(expectedResponse, actualResponse);
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

        public static HttpClient GetDefaultHttpClient(HttpMessageHandler httpMessageHandler) =>
            new HttpClient(httpMessageHandler)
            {
                BaseAddress = new Uri("http://localhost/")
            };

    }
}
