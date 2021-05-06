using Cowin.Watch.Core.ApiClient;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Cowin.Watch.Core
{
    public class CowinApiHttpClient : ICowinApiClient
    {
        private HttpClient httpClient;

        public CowinApiHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Root> GetSessionsForDistrictAndDateAsync(DistrictId districtId, DateTimeOffset dateFrom, CancellationToken cancellationToken)
        {
            string requestUri = $"appointment/sessions/public/calendarByDistrict?district_id={districtId}&date={dateFrom:d}";
            using (HttpRequestMessage hrm = new HttpRequestMessage(HttpMethod.Get, requestUri)) {
                var response = await httpClient.SendAsync(hrm, cancellationToken);

                if (!response.IsSuccessStatusCode) {
                    if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden) {
                        throw new UnauthorizedAPIAccessException();
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound) {
                        throw new NotFoundAPIException();
                    }
                }

                string responseContent = await response.Content.ReadAsStringAsync();
                if (response.Content.Headers.ContentType.MediaType != "application/json") {
                    throw new UnexpectedResponseException();
                }
                return JsonSerializer.Deserialize<Root>(responseContent);
            }

        }
    }
}