using Cowin.Watch.Core.ApiClient;
using Cowin.Watch.Core.Tests.Lib.HttpClientHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Cowin.Watch.Core.Tests
{
    [TestClass]
    public class QueryTests
    {

        [TestMethod]
        public void Is_Query_Created()
        {
            var districtId = 15; var dateFrom = DateTimeOffset.Parse("04-May-2021");

            var cowinClient = ClientFactory.GetHandlerFor_204().GetSessionsForDistrictAndDate(districtId, dateFrom);
        }
    }

    public static class ClientFactory
    {
        public static ICowinApiClient GetCowinClientForHandler(DelegatingHandler delegatingHandler) => 
            new CowinApiHttpClient(new HttpClient(delegatingHandler));

        public static ICowinApiClient GetHandlerFor_204() =>
            new CowinApiHttpClient(new HttpClient(new NoContentResponseHandler()));

        public static ICowinApiClient GetHandlerFor_200<TContent>(TContent content) =>
            new CowinApiHttpClient(new HttpClient(OkResponseHandler<TContent>.ForContent<TContent>(content)));
    }
}
