using Cowin.Watch.Core.ApiClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core.Tests
{
    [TestClass]
    public class QueryIntegrationTests
    {

        [TestMethod]
        public async Task When_Request_Is_Valid_Endpoint_Returns_200()
        {
            var client = LiveClientFactory.GetClient();
            var districtId = 15; var dateFrom = DateTimeOffset.Parse("04-May-2021");

            var response = await client.GetSessionsForDistrictAndDateAsync(districtId, dateFrom, CancellationToken.None);
            Assert.IsNotNull(response);
        }
    }

    public class LiveClientFactory
    {
        public static ICowinApiClient GetClient() =>
            new CowinApiHttpClient(new System.Net.Http.HttpClient()
            {
                BaseAddress = new Uri("https://cdn-api.co-vin.in/api/v2/"),
                Timeout = TimeSpan.FromSeconds(30)
            });
    }
}
