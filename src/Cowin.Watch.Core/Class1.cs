using System;
using System.Text.Json.Serialization;

namespace Cowin.Watch.Core
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class VaccineFee
    {
        public VaccineFee(
            [JsonPropertyName("vaccine")] string vaccine,
            [JsonPropertyName("fee")] string fee
        )
        {
            this.Vaccine = vaccine;
            this.Fee = fee;
        }

        [JsonPropertyName("vaccine")]
        public string Vaccine { get; }

        [JsonPropertyName("fee")]
        public string Fee { get; }
    }

    public class Session
    {
        public Session(
            [JsonPropertyName("session_id")] string sessionId,
            [JsonPropertyName("date")] string date,
            [JsonPropertyName("available_capacity")] int availableCapacity,
            [JsonPropertyName("min_age_limit")] int minAgeLimit,
            [JsonPropertyName("vaccine")] string vaccine,
            [JsonPropertyName("slots")] List<string> slots
        )
        {
            this.SessionId = sessionId;
            this.Date = date;
            this.AvailableCapacity = availableCapacity;
            this.MinAgeLimit = minAgeLimit;
            this.Vaccine = vaccine;
            this.Slots = slots;
        }

        [JsonPropertyName("session_id")]
        public string SessionId { get; }

        [JsonPropertyName("date")]
        public string Date { get; }

        [JsonPropertyName("available_capacity")]
        public int AvailableCapacity { get; }

        [JsonPropertyName("min_age_limit")]
        public int MinAgeLimit { get; }

        [JsonPropertyName("vaccine")]
        public string Vaccine { get; }

        [JsonPropertyName("slots")]
        public IReadOnlyList<string> Slots { get; }
    }

    public class Center
    {
        public Center(
            [JsonPropertyName("center_id")] int centerId,
            [JsonPropertyName("name")] string name,
            [JsonPropertyName("name_l")] string nameL,
            [JsonPropertyName("state_name")] string stateName,
            [JsonPropertyName("state_name_l")] string stateNameL,
            [JsonPropertyName("district_name")] string districtName,
            [JsonPropertyName("district_name_l")] string districtNameL,
            [JsonPropertyName("block_name")] string blockName,
            [JsonPropertyName("block_name_l")] string blockNameL,
            [JsonPropertyName("pincode")] string pincode,
            [JsonPropertyName("lat")] double lat,
            [JsonPropertyName("long")] double @long,
            [JsonPropertyName("from")] string from,
            [JsonPropertyName("to")] string to,
            [JsonPropertyName("fee_type")] string feeType,
            [JsonPropertyName("vaccine_fees")] List<VaccineFee> vaccineFees,
            [JsonPropertyName("sessions")] List<Session> sessions
        )
        {
            this.CenterId = centerId;
            this.Name = name;
            this.NameL = nameL;
            this.StateName = stateName;
            this.StateNameL = stateNameL;
            this.DistrictName = districtName;
            this.DistrictNameL = districtNameL;
            this.BlockName = blockName;
            this.BlockNameL = blockNameL;
            this.Pincode = pincode;
            this.Lat = lat;
            this.Long = @long;
            this.From = from;
            this.To = to;
            this.FeeType = feeType;
            this.VaccineFees = vaccineFees;
            this.Sessions = sessions;
        }

        [JsonPropertyName("center_id")]
        public int CenterId { get; }

        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("name_l")]
        public string NameL { get; }

        [JsonPropertyName("state_name")]
        public string StateName { get; }

        [JsonPropertyName("state_name_l")]
        public string StateNameL { get; }

        [JsonPropertyName("district_name")]
        public string DistrictName { get; }

        [JsonPropertyName("district_name_l")]
        public string DistrictNameL { get; }

        [JsonPropertyName("block_name")]
        public string BlockName { get; }

        [JsonPropertyName("block_name_l")]
        public string BlockNameL { get; }

        [JsonPropertyName("pincode")]
        public string Pincode { get; }

        [JsonPropertyName("lat")]
        public double Lat { get; }

        [JsonPropertyName("long")]
        public double Long { get; }

        [JsonPropertyName("from")]
        public string From { get; }

        [JsonPropertyName("to")]
        public string To { get; }

        [JsonPropertyName("fee_type")]
        public string FeeType { get; }

        [JsonPropertyName("vaccine_fees")]
        public IReadOnlyList<VaccineFee> VaccineFees { get; }

        [JsonPropertyName("sessions")]
        public IReadOnlyList<Session> Sessions { get; }
    }

    public class Root
    {
        public Root(
            [JsonPropertyName("centers")] List<Center> centers
        )
        {
            this.Centers = centers;
        }

        [JsonPropertyName("centers")]
        public IReadOnlyList<Center> Centers { get; }
    }


}
