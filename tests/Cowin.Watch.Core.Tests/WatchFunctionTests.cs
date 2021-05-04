using Cowin.Watch.Core.Tests.Lib;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cowin.Watch.Core.Tests
{
    [TestClass]
    class WatchFunctionTests
    {
        [TestMethod]
        public void When_WatchFunction_Starts_Logging_Works()
        {
            var logger = GetLogger();
            Cowin.Watch.Function.Watch.Run(null, logger);

            Assert.IsNotNull(logger);

            var logs = (logger as ListLogger).Logs;
            Assert.IsTrue( logs[1].Contains("Finding"));

        }

        private ILogger GetLogger() => new ListLogger();

}
