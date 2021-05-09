using System;

namespace Cowin.Watch.Core.Tests.Lib
{
    public static class SampleJsonFactory
    {
        public static string GenerateResponseForVaccineWithSlots(VaccineType vaccineType)
        {
            return GenerateResponseWithSessions("District General Hostpital", vaccineType, 50, DateTimeOffset.Now);
        }

        public static string GenerateResponseForHospitalAndVaccineWithSlots(string hospitalName, VaccineType vaccineType)
        {
            return GenerateResponseWithSessions(hospitalName, vaccineType, 50, DateTimeOffset.Now);
        }

        public static string GenerateResponseWithSessions(string hospitalName, VaccineType vaccineType, int capacity, DateTimeOffset sessionDate)
        {
            string rawJson = @"
{
  ""centers"": [
    {
      ""center_id"": 1234,
      ""name"": ""##_HOSPITAL_##"",
      ""name_l"": """",
      ""state_name"": ""Maharashtra"",
      ""state_name_l"": """",
      ""district_name"": ""Satara"",
      ""district_name_l"": """",
      ""block_name"": ""Jaoli"",
      ""block_name_l"": """",
      ""pincode"": 413608,
      ""lat"": 28.7,
      ""long"": 77.1,
      ""from"": ""09:00:00"",
      ""to"": ""18:00:00"",
      ""fee_type"": ""Free"",
      ""vaccine_fees"": [
        {
          ""vaccine"": ""##_Vaccine_##"",
          ""fee"": ""250""
        }
      ],
      ""sessions"": [
        {
          ""session_id"": ""3fa85f64-5717-4562-b3fc-2c963f66afa6"",
          ""date"": ""##_Date_##"",
          ""available_capacity"": ##_Capacity_##,
          ""min_age_limit"": 18,
          ""vaccine"": ""##_Vaccine_##"",
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
            return rawJson
                .Replace("##_HOSPITAL_##", hospitalName)
                .Replace("##_Vaccine_##", vaccineType.ToString())
                .Replace("##_Capacity_##", capacity.ToString())
                .Replace("##_Date_##", sessionDate.ToString("d"));
        }

        public static string GenerateResponseForVaccineWithoutSlots(VaccineType vaccineType)
        {
            return GenerateResponseWithoutSessions("District General Hostpital");
        }

        public static string GenerateResponseForHospitalAndVaccineWithoutSlots(string hospitalName)
        {
            return GenerateResponseWithoutSessions(hospitalName);
        }

        public static string GenerateResponseForHospitalWithoutSlots(string hospital)
        {
            return GenerateResponseWithoutSessions(hospital);
        }

        private static string GenerateResponseWithoutSessions(string hospitalName)
        {
            string rawJson = @"
{
  ""centers"": [
    {
      ""center_id"": 1234,
      ""name"": ""##_HOSPITAL_##"",
      ""name_l"": """",
      ""state_name"": ""Maharashtra"",
      ""state_name_l"": """",
      ""district_name"": ""Satara"",
      ""district_name_l"": """",
      ""block_name"": ""Jaoli"",
      ""block_name_l"": """",
      ""pincode"": 413608,
      ""lat"": 28.7,
      ""long"": 77.1,
      ""from"": ""09:00:00"",
      ""to"": ""18:00:00"",
      ""fee_type"": ""Free"",
      ""sessions"": [
      ]
    }
  ]
}
";
            return rawJson
                .Replace("##_HOSPITAL_##", hospitalName);
        }

        public static string GetDefaultCentersApiResponseJson()
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
      ""pincode"": 413608,
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
