﻿using Cowin.Watch.Core;
using Cowin.Watch.Function.Config;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System;
using System.Net.Http.Headers;

[assembly: FunctionsStartup(typeof(Cowin.Watch.Function.Startup))]

namespace Cowin.Watch.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient<CowinApiHttpClient>(client => 
            {
                client.BaseAddress = EnvVariables.CowinBaseUrl();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.UserAgent
                .Add(new ProductInfoHeaderValue("cowin-watch", "0.7.0"));
                client.DefaultRequestHeaders.AcceptLanguage
                .Add(new StringWithQualityHeaderValue("en-US", 0.9));
            });
        }
    }
}
