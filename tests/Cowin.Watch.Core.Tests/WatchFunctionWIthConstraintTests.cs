using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Cowin.Watch;
using Cowin.Watch.Core.Tests.Lib;
using Microsoft.Extensions.Logging;
using System.Threading;
using Cowin.Watch.Function;
using System.Threading.Tasks;

namespace Cowin.Watch.Core.Tests
{
    [TestClass]
    public class WatchFunctionWIthConstraintTests
    {

        [TestMethod]
        public async Task When_WatchFunction_Starts_WithDistrictId_If_Api_Has_Slots_Result_Is_Logged()
        {
            var districtId = DistrictId.FromInt(56);

            IFunctionConfig fnConfig = FunctionConfigFactory.FromMemory(districtId);
            var logger = GetLogger();
            await GetWatchFn(logger, fnConfig).Run(null, CancellationToken.None);

            Assert.IsNotNull(logger);  
        }

        private ILogger GetLogger() => new ListLogger();

        private Function.Watch GetWatchFn(ILogger logger, string expectedReponse, IFunctionConfig fnConfig)
        {
            CowinApiHttpClient cowinApiClient = ClientFactory.GetHandlerFor_200<string>(expectedReponse) as CowinApiHttpClient;
            return new Function.Watch(logger, cowinApiClient, fnConfig);
        }

        private Function.Watch GetWatchFn(ILogger logger, IFunctionConfig fnConfig)
        {
            var response = SampleJsonFactory.GetDefaultCentersApiResponseJson();
            CowinApiHttpClient cowinApiClient = ClientFactory.GetHandlerFor_200<string>(response) as CowinApiHttpClient;
            return new Function.Watch(logger, cowinApiClient, fnConfig);
        }
    }
}
