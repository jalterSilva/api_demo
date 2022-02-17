using GSP.API.Application.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GSP.API.Application
{
    public class Startup
    {
        #region Properties
        public IConfiguration _configuration { get; }
        public IWebHostEnvironment _environment { get; }
        #endregion

        #region Constructor
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }
        #endregion

        #region Configure Services
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddApplicationInsightsTelemetry();

            services.AddControllers();

            services.AddRateLimitConfiguration(_configuration);

            services.Configure<Core.Helpers.AppSettings>(_configuration.GetSection("AppSettings"));
            services.Configure<Core.Helpers.ConnectionString>(_configuration.GetSection("ConnectionString"));

            services.AddSwaggerConfiguration();

            services.AddAuthenticationConfiguration(_configuration);

            services.AddCompressionConfiguration();

            services.AddHttpContextAccessor();

            services.AddAuthorizationConfiguration();

            services.AddDependencyInjectionConfiguration();

            services.AddAutoMapper(typeof(Startup));         
        }
        #endregion

        #region Configure
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerConfiguration(env);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseDeveloperExceptionPage();

            app.UseNWebsecConfiguration();

            app.UseFeaturePolicyConfiguration();

            app.UseResponseCompression();

            app.UseStaticFiles();

            app.UseRateLimitConfiguration();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        #endregion
    }
}
