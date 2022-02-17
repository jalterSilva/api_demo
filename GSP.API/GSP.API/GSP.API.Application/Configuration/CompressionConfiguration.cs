using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;

namespace GSP.API.Application.Configuration
{
    public static class CompressionConfiguration
    {
        public static void AddCompressionConfiguration(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    var serializerOptions = options.JsonSerializerOptions;
                    serializerOptions.IgnoreNullValues = true;
                });
        }
    }
}
