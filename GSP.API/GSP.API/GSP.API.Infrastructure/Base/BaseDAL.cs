using Dapper;
using GSP.API.Core.Helpers;
using GSP.API.Core.Models.Application;
using GSP.API.Core.Models.Base;
using GSP.API.Infrastructure.DBSession;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GSP.API.Infrastructure.Base
{
    public abstract class BaseDAL
    {
        #region Properties
        public readonly DbSessionAPI _apiSession;
        public readonly DbSessionAPIExternal _apiExternalSession;
        private readonly IConfiguration _configuration;
        public readonly IEnumerable<string> _userRoles;
        public readonly ClaimsModel _claims;       
        public DynamicParameters _dynamicParameters;
        public readonly AppSettings _appSettings;
        #endregion

        #region Constructor
        public BaseDAL(IConfiguration configuration,
                       IHttpContextAccessor httpContextAccessor,
                       IOptions<AppSettings> appSettings,
                       DbSessionAPI mySqlSession,
                       DbSessionAPIExternal sqlServerSession)
        {
            _appSettings = appSettings.Value;
            _configuration = configuration;
            _apiSession = mySqlSession;
            _apiExternalSession = sqlServerSession;

            if (httpContextAccessor != null && httpContextAccessor.HttpContext.User.Claims.Count() > 0)
            {
                _userRoles = httpContextAccessor.HttpContext.User.Claims
                                                .Where(c => c.Type.Equals(ClaimTypes.Role))
                                                .ToList()
                                                .Select(x => x.Value);

                ClaimsIdentity identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

                if (identity != null && identity.Claims.Count() > 0)
                {
                    _claims = new ClaimsModel();
                    _claims.UserId = identity.FindFirst("UserId") != null ? int.Parse(identity.FindFirst("UserId").Value) : 0;
                    _claims.UserName = identity.FindFirst("UserName")?.Value;
                    _claims.FirstName = identity.FindFirst("FirstName")?.Value;
                    _claims.LastName = identity.FindFirst("LastName")?.Value;
                    _claims.CurrentHerdCode = identity.FindFirst("CurrentHerdCode")?.Value;
                }
            }
            else
            {
                _userRoles = new List<string>();
            }
        }
        #endregion

        #region Validate Model State
        public void ValidateModelState<T>(T model) where T : BaseModel
        {
            if (model?.ValidationResult is null)
                throw new ArgumentNullException("The model was not validated");

            if (!model.ValidationResult.IsValid)
                throw new ArgumentNullException("The model state is not valid");
        } 
        #endregion
    }
}
