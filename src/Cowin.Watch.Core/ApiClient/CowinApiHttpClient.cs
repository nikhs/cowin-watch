using Cowin.Watch.Core.ApiClient;
using System;
using System.Net.Http;

namespace Cowin.Watch.Core
{
    public class CowinApiHttpClient : ICowinApiClient
    {
        private HttpClient httpClient;

        public CowinApiHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public object GetSessionsForDistrictAndDate(int districtId, DateTimeOffset dateFrom)
        {
            throw new NotImplementedException();
        }
    }
}