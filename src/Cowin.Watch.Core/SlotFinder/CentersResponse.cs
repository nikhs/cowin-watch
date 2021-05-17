using System;
using System.Collections.Generic;
using System.Linq;

namespace Cowin.Watch.Core
{
    public interface ICentersResponse
    {
        bool HasSessions { get; }

        bool HasVaccine(VaccineType expectedVaccine);
        void ForEach(Action<Center> action);
        void None(Action noneAction);
    }

    public class CentersResponseFactory
    {
        public static ICentersResponse GetFor(IEnumerable<Center> centers)
        {
            if (IsEmpty(centers)) {
                return GetEmpty();
            }

            CentersWithSessions centersWithSessions = CentersWithSessions.From(centers);

            if (centersWithSessions.Any()) return centersWithSessions;
            return CentersWithoutSessions.From(centers);
        }


        private static bool IsEmpty(IEnumerable<Center> centers)
        {
            return !centers?.Any() ?? true;
        }

        public static ICentersResponse GetEmpty() => NullCenters.Instance;
    }

    public class CentersWithSessions : ICentersResponse
    {
        private readonly IEnumerable<Center> centersWithSessions;


        public CentersWithSessions(IEnumerable<Center> centersWithSessions)
        {
            this.centersWithSessions = centersWithSessions;
        }

        public bool HasSessions => true;
        public bool HasVaccine(VaccineType expectedVaccine) => centersWithSessions.Any(c => c.Sessions.Any(s => expectedVaccine.Equals(s.Vaccine ?? string.Empty)));

        public void ForEach(Action<Center> action)
        {
            foreach (var center in centersWithSessions) {
                action(center);
            }
        }

        public void None(Action noneAction) { }

        public static CentersWithSessions From(IEnumerable<Center> centers)
        {
            Validate(centers);
            var centersWithSessions = GeCentersWithSessions(centers);
            return new CentersWithSessions(centersWithSessions);
        }

        private static IEnumerable<Center> GeCentersWithSessions(IEnumerable<Center> centers)
        {
            return centers.Where(center => center.Sessions != null && center.Sessions.Count > 0);
        }

        private static void Validate(IEnumerable<Center> centers)
        {
            if (! centers.Any()) throw new ArgumentNullException(nameof(centers));
        }

        public bool Any() => centersWithSessions?.Any() ?? false;
    }

    public class CentersWithoutSessions : ICentersResponse
    {
        private readonly IEnumerable<Center> centersWithoutSession;

        public CentersWithoutSessions(IEnumerable<Center> centersWithoutSession)
        {
            this.centersWithoutSession = centersWithoutSession;
        }

        public bool HasSessions => false;
        public bool HasVaccine(VaccineType expectedVaccine) => false;
        public void ForEach(Action<Center> action) { }
        public void None(Action noneAction) => noneAction();


        public static CentersWithoutSessions From(IEnumerable<Center> centers)
        {
            Validate(centers);
            var centersWithoutSessions = GeCentersWithoutSessions(centers);
            return new CentersWithoutSessions(centersWithoutSessions);
        }

        private static IEnumerable<Center> GeCentersWithoutSessions(IEnumerable<Center> centers)
        {
            return centers.Where(center => center.Sessions == null || center.Sessions.Count == 0);
        }

        private static void Validate(IEnumerable<Center> centers)
        {
            if (!centers.Any()) throw new ArgumentNullException(nameof(centers));
        }
    }

    public class NullCenters : ICentersResponse
    {
        private static readonly ICentersResponse instance = new NullCenters();
        public static ICentersResponse Instance => instance;

        private NullCenters()
        {

        }

        public bool HasSessions => false;
        public bool HasVaccine(VaccineType expectedVaccine) => false;
        public void ForEach(Action<Center> action) { }
        public void None(Action noneAction) => noneAction();
    }
}