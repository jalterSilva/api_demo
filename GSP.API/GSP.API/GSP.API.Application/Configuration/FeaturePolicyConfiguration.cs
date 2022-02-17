using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Primitives;

namespace GSP.API.Application.Configuration
{
    public static class FeaturePolicyConfiguration
    {
        public static void UseFeaturePolicyConfiguration(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Feature-Policy", new StringValues(
                    "accelerometer 'none';" +
                    "ambient-light-sensor 'none';" +
                    "autoplay 'none';" +
                    "battery 'none';" +
                    "camera 'none';" +
                    "display-capture 'none';" +
                    "document-domain 'none';" +
                    "encrypted-media 'none';" +
                    "execution-while-not-rendered 'none';" +
                    "execution-while-out-of-viewport 'none';" +
                    "gyroscope 'none';" +
                    "magnetometer 'none';" +
                    "microphone 'none';" +
                    "midi 'none';" +
                    "navigation-override 'none';" +
                    "payment 'none';" +
                    "picture-in-picture 'none';" +
                    "publickey-credentials-get 'none';" +
                    "sync-xhr 'none';" +
                    "usb 'none';" +
                    "wake-lock 'none';" +
                    "xr-spatial-tracking 'none';"
                ));

                await next.Invoke();
            });
        }
    }
}
