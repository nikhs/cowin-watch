using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cowin.Watch.Core.ApiClient
{
    public interface ICowinApiClient
    {
        Task<Root> GetSessionsForDistrictAndDateAsync(int districtId, DateTimeOffset dateFrom);
    }
}
