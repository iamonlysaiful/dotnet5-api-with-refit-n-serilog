using DotNet5APIDemo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;
using Serilog;
using System;

namespace DotNet5APIDemo.Extensions
{
    public static class RefitPollyExtensions
    {
        public static IServiceCollection AddRefitPollyForPullClient(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpClient("pull_client", c =>
            {
                c.BaseAddress = new(config["ReqRes:Url"]);
            })
            .AddTypedClient(c => RestService.For<IHttpClient>(c))
            .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(int.Parse(config["ReqRes:Retry"]), _ => TimeSpan.FromMilliseconds(int.Parse(config["ReqRes:Wait"])), (result, timeSpan, retryCount, context) =>
            {
                Log.Warning($"Request failed: {result.Exception.Message}. Wait {timeSpan.TotalSeconds}s before retry. Retry attempt {retryCount}.");
            }));

            return services;
        }
    }
}

