using Cowin.Watch.Core.ApiClient;
using Microsoft.Extensions.Logging;
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
        private readonly HttpClient httpClient;
        private readonly ILogger<CowinApiHttpClient> logger;

        public CowinApiHttpClient(HttpClient httpClient, ILogger<CowinApiHttpClient> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<Root> GetSessionsForDistrictAndDateAsync(DistrictId districtId, DateTimeOffset dateFrom, CancellationToken cancellationToken)
        {
            string requestUri = $"appointment/sessions/public/calendarByDistrict?district_id={districtId}&date={dateFrom:d}";
            return await GetSessions(requestUri, cancellationToken);
        }

        public async Task<Root> GetSessionsForPincodeAndDateAsync(Pincode pincode, DateTimeOffset dateFrom, CancellationToken cancellationToken)
        {
            string requestUri = $"appointment/sessions/public/calendarByPin?pincode={pincode}&date={dateFrom:d}";
            return await GetSessions(requestUri, cancellationToken);
        }

        private async Task<Root> GetSessions(string requestUri, CancellationToken cancellationToken)
        {
            using (logger.BeginScope("{nameof(ICowinApiClient)}:", nameof(CowinApiHttpClient)))
            logger.LogDebug("Querying {requestUri}", requestUri);

            using HttpRequestMessage hrm = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await httpClient.SendAsync(hrm, cancellationToken);

            logger.LogDebug("Query result:{statusCode}", response.StatusCode);

            if (RequestHasJsonResponse(response)) {
                string responseContent = await response.Content.ReadAsStringAsync();
                var parsed = JsonSerializer.Deserialize<Root>(responseContent);
                return parsed ?? throw new UnexpectedResponseException();
            }

            switch (response.StatusCode) {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    throw new UnauthorizedApiAccessException();
                case HttpStatusCode.NotFound:
                    throw new NotFoundApiException();
                default:
                    throw new UnexpectedResponseException();
            }
        }

        private static bool RequestHasJsonResponse(HttpResponseMessage response)
        {
            return response.StatusCode == HttpStatusCode.OK && 
                response.Content.Headers.ContentType.MediaType == "application/json";
        }
    }
}