using Cowin.Watch.Core.Tests.Lib;
using Cowin.Watch.Function;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core.Tests
{
    [TestClass]
    public class WatchFunctionWIthConstraintTests
    {

        [TestMethod]
        public async Task WhenWatchFunction_Starts_WithDistrictId_If_Api_Has_Slots_Result_Is_Logged()
        {
            var districtId = "56";
            IFunctionConfig fnConfig = FunctionConfigFactory.FromMemory(districtId, string.Empty, string.Empty);
            var logger = GetLogger<Watch.Function.Watch>();

            await GetWatchFn(logger, fnConfig).Run(null, CancellationToken.None);

            Assert.IsNotNull(logger);
        }

        private ILogger GetLogger() => new ListLogger();
        private ILogger<T> GetLogger<T>() where T : class => new ListLogger<T>();

        private Function.Watch GetWatchFn<T>(ILogger<Watch.Function.Watch> logger, string expectedReponse, IFunctionConfig fnConfig)
        {
            CowinApiHttpClient cowinApiClient = ClientFactory.GetHandlerFor_200<string>(expectedReponse) as CowinApiHttpClient;
            return new Function.Watch(logger, cowinApiClient, fnConfig);
        }

        private Function.Watch GetWatchFn(ILogger<Watch.Function.Watch> logger, IFunctionConfig fnConfig)
        {
            var response = SampleJsonFactory.GetDefaultCentersApiResponseJson();
            CowinApiHttpClient cowinApiClient = ClientFactory.GetHandlerFor_200<string>(response) as CowinApiHttpClient;
            return new Function.Watch(logger, cowinApiClient, fnConfig);
        }
    }
}
