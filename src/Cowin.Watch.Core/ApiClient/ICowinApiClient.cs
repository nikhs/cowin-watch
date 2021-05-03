using System;
using System.Collections.Generic;
using System.Text;

namespace Cowin.Watch.Core.ApiClient
{
    public interface ICowinApiClient
    {
        object GetSessionsForDistrictAndDate(int districtId, DateTimeOffset dateFrom);
    }
}
