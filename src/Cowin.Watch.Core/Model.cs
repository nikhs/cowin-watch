using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cowin.Watch.Core
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class VaccineFee
    {
        [JsonPropertyName("vaccine")]
        public string Vaccine { get;  set; }

        [JsonPropertyName("fee")]
        public string Fee { get;  set; }
    }

    public class Session
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get;  set; }

        [JsonPropertyName("date")]
        public string Date { get;  set; }

        [JsonPropertyName("available_capacity")]
        public int AvailableCapacity { get;  set; }

        [JsonPropertyName("min_age_limit")]
        public int MinAgeLimit { get;  set; }

        [JsonPropertyName("vaccine")]
        public string Vaccine { get;  set; }

        [JsonPropertyName("slots")]
        public IReadOnlyList<string> Slots { get;  set; }
    }

    public class Center
    {
        [JsonPropertyName("center_id")]
        public int CenterId { get;  set; }

        [JsonPropertyName("name")]
        public string Name { get;  set; }

        [JsonPropertyName("name_l")]
        public string NameL { get;  set; }

        [JsonPropertyName("state_name")]
        public string StateName { get;  set; }

        [JsonPropertyName("state_name_l")]
        public string StateNameL { get;  set; }

        [JsonPropertyName("district_name")]
        public string DistrictName { get;  set; }

        [JsonPropertyName("district_name_l")]
        public string DistrictNameL { get;  set; }

        [JsonPropertyName("block_name")]
        public string BlockName { get;  set; }

        [JsonPropertyName("block_name_l")]
        public string BlockNameL { get;  set; }

        [JsonPropertyName("pincode")]
        public long Pincode { get;  set; }

        [JsonPropertyName("lat")]
        public double Lat { get;  set; }

        [JsonPropertyName("long")]
        public double Long { get;  set; }

        [JsonPropertyName("from")]
        public string From { get;  set; }

        [JsonPropertyName("to")]
        public string To { get;  set; }

        [JsonPropertyName("fee_type")]
        public string FeeType { get;  set; }

        [JsonPropertyName("vaccine_fees")]
        public IReadOnlyList<VaccineFee> VaccineFees { get;  set; }

        [JsonPropertyName("sessions")]
        public IReadOnlyList<Session> Sessions { get;  set; }
    }

    public class Root
    {
        [JsonPropertyName("centers")]
        public IReadOnlyList<Center> Centers { get;  set; }
    }


}
