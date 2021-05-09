using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core.ApiClient
{
    public interface ICowinApiClient
    {
        Task<Root> GetSessionsForDistrictAndDateAsync(DistrictId districtId, DateTimeOffset dateFrom, System.Threading.CancellationToken token);
        Task<Root> GetSessionsForPincodeAndDateAsync(Pincode pincode, DateTimeOffset dateFrom, CancellationToken none);
    }
}
