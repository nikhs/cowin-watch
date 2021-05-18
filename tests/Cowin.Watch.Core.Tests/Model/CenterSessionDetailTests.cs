using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cowin.Watch.Core.Tests.Model
{
    [TestClass]
    public class CenterSessionDetailTests
    {
        [TestMethod]
        public void WhenCenterSessionDetailCreated_IEnumerableListIsCreated()
        {
            Center center = new Center()
            {
                Name = "a",
                Sessions = new List<Session>()
                {
                    new Session()
                    {
                        Vaccine = VaccineType.Covaxin().ToString()
                    }
                }
            };

            var detail = CenterSessionDetail.FromCenter(center);

            var expectedType = typeof(IEnumerable<CenterSessionDetail>);
            Assert.IsInstanceOfType(detail, expectedType);
        }

        public void WhenCenterSessionDetailCreated_WithInvalidSessionDate_IEnumerableListIsNotCreated()
        {
            Center center = new Center()
            {
                Name = "a"
            };

            var detail = CenterSessionDetail.FromCenter(center);

            var expectedType = typeof(IEnumerable<CenterSessionDetail>);
            Assert.IsInstanceOfType(detail, expectedType);
        }
    }
}
