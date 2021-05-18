using System;
using System.Collections.Generic;
using System.Linq;

namespace Cowin.Watch.Core
{
    public interface ICenterSessionDetail
    {
        public string CenterName { get; }
        public string CenterLocation { get; }
        public DateTimeOffset SessionDate { get; }
        public string SessionSlots { get; }
        public VaccineType SessionVaccine { get; }
    }

    public class CenterSessionDetail : ICenterSessionDetail
    {
        public string CenterName { get; }
        public string CenterLocation { get; }
        public DateTimeOffset SessionDate { get; }
        public string SessionSlots { get; }
        public VaccineType SessionVaccine { get; }

        public CenterSessionDetail(string centerName, string centerLocation, DateTimeOffset sessionDate, string sessionSlots, VaccineType sessionVaccine)
        {
            CenterName = centerName;
            CenterLocation = centerLocation;
            SessionDate = sessionDate;
            SessionSlots = sessionSlots;
            SessionVaccine = sessionVaccine;
        }


        internal static IEnumerable<CenterSessionDetail> FromCenter(Center center)
        {
            var sessionDetails = from session in center.Sessions
                                 select new {
                                     CenterName = center.Name,
                                     CenterLocation = center.BlockName,
                                     SessionDate = DateTimeOffset.Parse(session.Date),
                                     SessionSlots = string.Join(",", session.Slots ?? Enumerable.Empty<string>()),
                                     SessionVaccine = VaccineType.From(session.Vaccine ?? string.Empty)
                                 };

            return sessionDetails.Select(session =>
            new CenterSessionDetail(
                session.CenterName,
                session.CenterLocation,
                session.SessionDate,
                session.SessionSlots,
                session.SessionVaccine));
        }

        internal static IEnumerable<CenterSessionDetail> FromCenterRange(IEnumerable<Center> centers) => centers.SelectMany(c => FromCenter(c));

    }
}
