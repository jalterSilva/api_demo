using GSP.API.Core.Helpers.ClaimService;
using GSP.API.Core.Managers.Authentication;
using GSP.API.Core.Managers.Broker;
using GSP.API.Core.Managers.User;
using GSP.API.Core.Providers.Authentication;
using GSP.API.Core.Providers.Broker;
using GSP.API.Core.Providers.UnitOfWork;
using GSP.API.Core.Providers.User;
using GSP.API.Infrastructure.Authentication;
using GSP.API.Infrastructure.Broker;
using GSP.API.Infrastructure.DBSession;
using GSP.API.Infrastructure.UnitOfWork;
using GSP.API.Infrastructure.User;
using Microsoft.Extensions.DependencyInjection;

namespace GSP.API.Application.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            #region Managers
            services.AddTransient<IAuthenticationManager, AuthenticationManager>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IBrokerManager, BrokerManager>();
           
            #endregion

            #region Providers
            services.AddTransient<IAuthenticationDAL, AuthenticationDAL>();
            services.AddTransient<IUserDAL, UserDAL>();
            services.AddTransient<IBrokerDAL, BrokerDAL>();

            #endregion

            #region Services
            services.AddTransient<IClaimService, ClaimService>();
            #endregion

            #region Connection
            services.AddScoped<DbSessionAPI>();
            services.AddScoped<DbSessionAPIExternal>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion
        }
    }
}
