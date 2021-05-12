using Cowin.Watch.Core;
using Cowin.Watch.Function.Config;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

[assembly: FunctionsStartup(typeof(Cowin.Watch.Function.Startup))]
[assembly: InternalsVisibleTo("Cowin.Watch.Core.Tests")]

namespace Cowin.Watch.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IFunctionConfig>(_ => FunctionConfigFactory.FromEnvironment());

            builder.Services.AddHttpClient<CowinApiHttpClient>(client => {
                client.BaseAddress = EnvironmentConfigSource.Get().CowinBaseUrl();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.UserAgent
                .Add(new ProductInfoHeaderValue("cowin-watch", "1.2.0"));
                client.DefaultRequestHeaders.AcceptLanguage
                .Add(new StringWithQualityHeaderValue("en-US", 0.9));
            });

        }
    }
}
