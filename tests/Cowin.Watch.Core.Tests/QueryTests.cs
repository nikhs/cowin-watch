using Cowin.Watch.Core.Tests.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core.Tests
{
    [TestClass]
    public class QueryTests
    {

        [TestMethod]
        public void Does_Client_District_Query_Handle_204()
        {
            var districtId = DistrictId.FromInt(15);
            var dateFrom = DateTimeOffset.Parse("04-May-2021");
            var cowinApiClient = ClientFactory.GetHandlerFor_204();

            Assert.ThrowsExceptionAsync<UnexpectedResponseException>(async () => await cowinApiClient.GetSessionsForDistrictAndDateAsync(districtId, dateFrom, CancellationToken.None));
        }

        [TestMethod]
        public async Task Does_Client_District_Query_Parse_Valid_Content()
        {
            var districtId = DistrictId.FromInt(15);
            var dateFrom = DateTimeOffset.Parse("04-May-2021");
            string content = SampleJsonFactory.GetDefaultCentersApiResponseJson();
            var cowinApiClient = ClientFactory.GetHandlerFor_200(content);

            Root actualResponse = await cowinApiClient.GetSessionsForDistrictAndDateAsync(districtId, dateFrom, CancellationToken.None);
            Assert.IsNotNull(actualResponse);
        }

        [TestMethod]
        public void When_District_Request_Is_Cancelled_An_AggregateException_Is_Thrown()
        {
            var districtId = DistrictId.FromInt(15);
            var dateFrom = DateTimeOffset.Parse("04-May-2021");
            var cowinApiClient = ClientFactory.GetHandlerFor_DelayedResponse();
            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(2));

            Assert.ThrowsExceptionAsync<AggregateException>(async () =>
                await cowinApiClient.GetSessionsForDistrictAndDateAsync(districtId, dateFrom, cancellationTokenSource.Token));
        }

        [TestMethod]
        public void Does_Client_Pincode_Query_Handle_204()
        {
            var pincode = Pincode.FromString("561234");
            var dateFrom = DateTimeOffset.Parse("04-May-2021");
            var cowinApiClient = ClientFactory.GetHandlerFor_204();

            Assert.ThrowsExceptionAsync<UnexpectedResponseException>(async () => await cowinApiClient.GetSessionsForPincodeAndDateAsync(pincode, dateFrom, CancellationToken.None));
        }

        [TestMethod]
        public async Task Does_Client_Pincode_Query_Parse_Valid_Content()
        {
            var pincode = Pincode.FromString("561234");
            var dateFrom = DateTimeOffset.Parse("04-May-2021");
            var cowinApiClient = ClientFactory.GetDefaultHandlerFor_200();

            Root actualResponse = await cowinApiClient.GetSessionsForPincodeAndDateAsync(pincode, dateFrom, CancellationToken.None);

            Assert.IsNotNull(actualResponse);
        }

        [TestMethod]
        public void When_Pincode_Request_Is_Cancelled_An_AggregateException_Is_Thrown()
        {
            var pincode = Pincode.FromString("561234");
            var dateFrom = DateTimeOffset.Parse("04-May-2021");
            var cowinApiClient = ClientFactory.GetHandlerFor_DelayedResponse();
            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(2));

            Assert.ThrowsExceptionAsync<AggregateException>(async () =>
                await cowinApiClient.GetSessionsForPincodeAndDateAsync(pincode, dateFrom, cancellationTokenSource.Token));
        }
    }
}
