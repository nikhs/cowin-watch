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

    static class SampleJsonFactory
    {
        public static string GetCentersApiResponseJson()
        {
            return @"
{
  ""centers"": [
    {
      ""center_id"": 1234,
      ""name"": ""District General Hostpital"",
      ""name_l"": """",
      ""state_name"": ""Maharashtra"",
      ""state_name_l"": """",
      ""district_name"": ""Satara"",
      ""district_name_l"": """",
      ""block_name"": ""Jaoli"",
      ""block_name_l"": """",
      ""pincode"": ""413608"",
      ""lat"": 28.7,
      ""long"": 77.1,
      ""from"": ""09:00:00"",
      ""to"": ""18:00:00"",
      ""fee_type"": ""Free"",
      ""vaccine_fees"": [
        {
          ""vaccine"": ""COVISHIELD"",
          ""fee"": ""250""
        }
      ],
      ""sessions"": [
        {
          ""session_id"": ""3fa85f64-5717-4562-b3fc-2c963f66afa6"",
          ""date"": ""31-05-2021"",
          ""available_capacity"": 50,
          ""min_age_limit"": 18,
          ""vaccine"": ""COVISHIELD"",
          ""slots"": [
            ""FORENOON"",
            ""AFTERNOON""
          ]
        }
      ]
    }
  ]
}
";
        }
    }
}
