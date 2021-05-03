using Cowin.Watch.Core.Tests.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text.Json;

namespace Cowin.Watch.Core.Tests
{
    [TestClass]
    public class JsonParseTests
    {
        [TestMethod]
        public void Centers_Are_Identified()
        {
            var src = SampleJsonFactory.GetCentersApiResponseJson();
            var root = JsonParser.DeserializeJson(src);

            Assert.IsNotNull(root.Centers);
        }

        [TestMethod]
        public void Number_Of_Centers_Found_Is_Correct()
        {
            var src = SampleJsonFactory.GetCentersApiResponseJson();
            var root = JsonParser.DeserializeJson(src);

            var expectedCentersCount = 1;
            Assert.AreEqual(root.Centers.Count, expectedCentersCount);
        }

        [TestMethod]
        public void Center_District_Is_Correct()
        {
            var src = SampleJsonFactory.GetCentersApiResponseJson();
            var root = JsonParser.DeserializeJson(src);

            var expectedDistrictName = "Satara";
            Center center = root.Centers.First();
            Assert.AreEqual(center.DistrictName, expectedDistrictName);
        }

        [TestMethod]
        public void Center_Sessions_Are_Identified()
        {
            var src = SampleJsonFactory.GetCentersApiResponseJson();
            var root = JsonParser.DeserializeJson(src);

            Center center = root.Centers.First();
            Assert.IsNotNull(center.Sessions);
        }

        [TestMethod]
        public void Number_Of_Center_Sessions_Found_Is_Correct()
        {
            var src = SampleJsonFactory.GetCentersApiResponseJson();
            var root = JsonParser.DeserializeJson(src);

            var expectedSessionCount = 1;
            Center center = root.Centers.First();
            Assert.AreEqual(center.Sessions.Count, expectedSessionCount);
        }

        [TestMethod]
        public void Ensure_Vaccine_Name_Is_Expected()
        {
            var src = SampleJsonFactory.GetCentersApiResponseJson();
            var root = JsonParser.DeserializeJson(src);

            var expectedVaccineNames = new string[] { "COVISHIELD", "COVAXIN" };
            bool IsVaccineValid(string vaccine)
            {
                return expectedVaccineNames.Any(v => v.Equals(vaccine, System.StringComparison.OrdinalIgnoreCase));
            }
            var vaccine = root.Centers.First().Sessions.First().Vaccine;
            Assert.IsTrue(IsVaccineValid(vaccine));
        }

        [TestMethod]
        public void Center_Sessions_Slots_Is_Not_Empty()
        {
            var src = SampleJsonFactory.GetCentersApiResponseJson();
            var root = JsonParser.DeserializeJson(src);

            var slots = root.Centers.First().Sessions.First().Slots;
            Assert.IsNotNull(slots);
        }
    }


    static class JsonParser
    {
        public static Root DeserializeJson(string json)
            => JsonSerializer.Deserialize<Root>(json);
    }
}
