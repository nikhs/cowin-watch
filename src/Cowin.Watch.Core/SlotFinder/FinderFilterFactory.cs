using System;

namespace Cowin.Watch.Core
{
    public class FinderFilterFactory
    {
        public static IFinderFilter From(DateTimeOffset dateFrom) => new DateOnlyFinder(dateFrom);
        public static IFinderFilter From(DateTimeOffset dateFrom, VaccineType vaccine) => new VaccineFinder(vaccine, dateFrom);
    }
}