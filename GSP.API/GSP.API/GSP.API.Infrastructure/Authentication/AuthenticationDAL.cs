using Dapper;
using GSP.API.Core.Helpers;
using GSP.API.Core.Models.Authentication;
using GSP.API.Core.Models.User;
using GSP.API.Core.Providers.Authentication;
using GSP.API.Infrastructure.Base;
using GSP.API.Infrastructure.DBSession;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSP.API.Infrastructure.Authentication
{
    public class AuthenticationDAL : BaseDAL, IAuthenticationDAL
    {
        #region Constructor
        public AuthenticationDAL(IConfiguration configuration,
                                 IHttpContextAccessor httpContextAccessor,
                                 IOptions<AppSettings> appSettings,
                                 DbSessionAPI mySqlSession,
                                 DbSessionAPIExternal sqlServerSession)
           : base(configuration, httpContextAccessor, appSettings, mySqlSession, sqlServerSession)
        {
        }
        #endregion

        #region GET

        #region Validate Login
        public async Task<UserModel> ValidateLogin(AuthenticationModel authentication)
        {
            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" SELECT us.Id, ");
            _sqlBuilder.AppendLine("        us.UserRoleId, ");
            _sqlBuilder.AppendLine("        us.UserName, ");
            _sqlBuilder.AppendLine("        us.PasswordHash, ");
            _sqlBuilder.AppendLine(" 	    us.Salt, ");
            _sqlBuilder.AppendLine("        us.Firstname, ");
            _sqlBuilder.AppendLine("        us.Lastname, ");
            _sqlBuilder.AppendLine("        us.PhoneNumber, ");
            _sqlBuilder.AppendLine("        us.Token, ");
            _sqlBuilder.AppendLine("        us.TokenDate, ");
            _sqlBuilder.AppendLine("        us.Active, ");
            _sqlBuilder.AppendLine("        us.ActiveDate, ");
            _sqlBuilder.AppendLine("        us.InactiveDate, ");
            _sqlBuilder.AppendLine("        us.CurrentHerdCode, ");
            _sqlBuilder.AppendLine("        us.CreatedDateTime, ");
            _sqlBuilder.AppendLine("        us.LastModifiedDateTime, ");
            _sqlBuilder.AppendLine("        ur.Id as UserRoleId, ");
            _sqlBuilder.AppendLine("        ur.RoleName ");
            _sqlBuilder.AppendLine(" FROM Users us ");
            _sqlBuilder.AppendLine(" INNER JOIN UserRole ur ");
            _sqlBuilder.AppendLine(" ON us.UserRoleId = ur.Id ");

            _sqlBuilder.AppendLine(" WHERE us.UserName = @UserName ");
            _sqlBuilder.AppendLine("    AND us.PasswordHash = HASHBYTES('SHA2_512', @Password + CAST(Salt AS NVARCHAR(36))) ");

            _dynamicParameters.Add("@UserName", authentication.UserName, DbType.String, ParameterDirection.Input, 70);
            _dynamicParameters.Add("@Password", authentication.Password, DbType.String, ParameterDirection.Input, 45);

            return (await _apiSession.Connection.QueryAsync<UserModel, UserRoleModel, UserModel>(_sqlBuilder.ToString(),
             (user, userRole) =>
             {
                 user.UserRole = userRole;

                 return user;
             },
             param: _dynamicParameters,
             transaction: _apiSession.Transaction,
             splitOn: "UserRoleId"
             )).FirstOrDefault();

        }
        #endregion

        #endregion
    }
}
