using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GSP.API.Application.Configuration
{
    public static class APIRateLimitConfiguration
    {

        public static void AddRateLimitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.AddMemoryCache();
            services.Configure<ClientRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            services.Configure<ClientRateLimitPolicies>(configuration.GetSection("IpRateLimiting"));
            services.AddInMemoryRateLimiting();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }

        public static void UseRateLimitConfiguration(this IApplicationBuilder app)
        {
            app.UseIpRateLimiting();
        }
    }
}

