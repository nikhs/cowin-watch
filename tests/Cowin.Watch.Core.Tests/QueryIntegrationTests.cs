using Cowin.Watch.Core.ApiClient;
using Cowin.Watch.Core.Tests.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core.Tests
{
    //[TestClass]
    public class QueryIntegrationTests
    {

        [TestMethod]
        public async Task When_District_Request_Is_Valid_Endpoint_Returns_200()
        {
            var client = LiveClientFactory.GetClient();
            var districtId = DistrictId.FromInt(19); var dateFrom = DateTimeOffset.Now;

            var response = await client.GetSessionsForDistrictAndDateAsync(districtId, dateFrom, CancellationToken.None);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task When_Pincode_Request_Is_Valid_Endpoint_Returns_200()
        {
            var client = LiveClientFactory.GetClient();
            var pincode = Pincode.FromString("123456"); var dateFrom = DateTimeOffset.Now;

            var response = await client.GetSessionsForPincodeAndDateAsync(pincode, dateFrom, CancellationToken.None);
            Assert.IsNotNull(response);
        }
    }

    public class LiveClientFactory
    {
        static readonly HttpClient liveHttpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://cdn-api.co-vin.in/api/v2/"),
            Timeout = TimeSpan.FromSeconds(30)
        };

        public static ICowinApiClient GetClient()
        {
            return new CowinApiHttpClient(liveHttpClient, new ListLogger<CowinApiHttpClient>());
        }
    }
}
