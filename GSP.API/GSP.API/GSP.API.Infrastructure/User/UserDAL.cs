using Dapper;
using Dapper.Contrib.Extensions;
using GSP.API.Core.Helpers;
using GSP.API.Core.Models.Enum;
using GSP.API.Core.Models.User;
using GSP.API.Core.Providers.User;
using GSP.API.Infrastructure.Base;
using GSP.API.Infrastructure.DBSession;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSP.API.Infrastructure.User
{
    public class UserDAL : BaseDAL, IUserDAL
    {
        #region Constructor
        public UserDAL(IConfiguration configuration,
                       IHttpContextAccessor httpContextAccessor,
                       IOptions<AppSettings> appSettings,
                       DbSessionAPI mySqlSession,
                       DbSessionAPIExternal sqlServerSession)
            : base(configuration, httpContextAccessor, appSettings, mySqlSession, sqlServerSession)
        {
        }
        #endregion

        #region CREATE

        #region Create New User
        public async Task<UserModel> CreateNewUser(UserModel user, string password)
        {
            ValidateModelState(user);

            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            var token = Guid.NewGuid();
            var salt = Guid.NewGuid().ToString().ToUpper();

            _sqlBuilder.AppendLine(" INSERT INTO Users ");
            _sqlBuilder.AppendLine(" (UserRoleId, UserName, PasswordHash, Salt, Firstname, Lastname, PhoneNumber, Token, TokenDate, ActiveDate, InactiveDate) ");
            _sqlBuilder.AppendLine(" OUTPUT INSERTED.Id ");
            _sqlBuilder.AppendLine(" VALUES(@UserRoleId, @UserName, HASHBYTES('SHA2_512', @Password + CAST(@Salt AS NVARCHAR(36))), @Salt, @Firstname, @Lastname, @PhoneNumber, @Token, (SELECT CONVERT(DATETIME, FORMAT(DATEADD(day, 1, getdate()), 'yyyy-MM-dd') + ' ' + '23:59:00.000')), NULL, NULL); ");

            _dynamicParameters.Add("@UserRoleId", user.UserRoleId, DbType.Int32, ParameterDirection.Input, 70);
            _dynamicParameters.Add("@UserName", user.UserName, DbType.String, ParameterDirection.Input, 70);
            _dynamicParameters.Add("@Password", password, DbType.String, ParameterDirection.Input, 45);
            _dynamicParameters.Add("@Salt", salt, DbType.String, ParameterDirection.Input, 36);
            _dynamicParameters.Add("@FirstName", user.FirstName, DbType.String, ParameterDirection.Input, 60);
            _dynamicParameters.Add("@LastName", user.LastName, DbType.String, ParameterDirection.Input, 60);
            _dynamicParameters.Add("@PhoneNumber", user.PhoneNumber, DbType.String, ParameterDirection.Input, 25);
            _dynamicParameters.Add("@Token", token, DbType.Guid, ParameterDirection.Input, 50);

            user.SetId(await _apiSession.Connection.ExecuteScalarAsync<int>(_sqlBuilder.ToString(), param: _dynamicParameters, transaction: _apiSession.Transaction));
            user.SetToken(token);

            return user;
        }

        #endregion

        #region Insert User Log
        public async Task InsertUserLog(UserLogModel userLog)
        {
            ValidateModelState(userLog);

            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" INSERT INTO UserLog (UserId, DeviceTypeId, UserLogActionId, ActionDate, ApplicationVersion, DeviceName, DeviceVersion, DevicePlatform, DeviceLatitude, DeviceLongitude, IP) ");
            _sqlBuilder.AppendLine(" VALUES (@UserId, @DeviceTypeId, @UserLogActionId, GETDATE(), @ApplicationVersion, @DeviceName, @DeviceVersion, @DevicePlatform, @DeviceLatitude, @DeviceLongitude, @IP) ");

            _dynamicParameters.Add("@UserId", userLog.UserId, DbType.Int32, ParameterDirection.Input);
            _dynamicParameters.Add("@DeviceTypeId", userLog.DeviceId, DbType.Int32, ParameterDirection.Input);
            _dynamicParameters.Add("@UserLogActionId", userLog.UserLogActionId, DbType.Int32, ParameterDirection.Input);
            _dynamicParameters.Add("@ApplicationVersion", userLog.ApplicationVersion, DbType.String, ParameterDirection.Input, 10);
            _dynamicParameters.Add("@DeviceName", userLog.DeviceName, DbType.String, ParameterDirection.Input, 30);
            _dynamicParameters.Add("@DeviceVersion", userLog.DeviceVersion, DbType.String, ParameterDirection.Input, 10);
            _dynamicParameters.Add("@DevicePlatform", userLog.DevicePlatform, DbType.String, ParameterDirection.Input, 10);
            _dynamicParameters.Add("@DeviceLatitude", userLog.DeviceLatitude, DbType.Double, ParameterDirection.Input);
            _dynamicParameters.Add("@DeviceLongitude", userLog.DeviceLongitude, DbType.Double, ParameterDirection.Input);
            _dynamicParameters.Add("@IP", userLog.IP, DbType.String, ParameterDirection.Input, 20);

            await _apiSession.Connection.ExecuteAsync(_sqlBuilder.ToString(), param: _dynamicParameters, transaction: _apiSession.Transaction);
        }
        #endregion

        #region Add User Log Action
        public async Task AddUserLogAction(UserLogModel userLog)
        {
            ValidateModelState(userLog);

            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" INSERT INTO UserLog (UserId, UserLogActionId, DeviceTypeId, ActionDate, ApplicationVersion, DeviceName, DeviceVersion, DevicePlatform, DeviceLatitude, DeviceLongitude, IP) ");
            _sqlBuilder.AppendLine(" VALUES (@UserId, @UserLogActionId, @DeviceTypeId, GETDATE(), @ApplicationVersion, @DeviceName, @DeviceVersion, @DevicePlatform, @DeviceLatitude, @DeviceLongitude, @IP) ");

            _dynamicParameters.Add("@UserId", userLog.UserId, DbType.Int32, ParameterDirection.Input);
            _dynamicParameters.Add("@UserLogActionId", userLog.UserLogActionId, DbType.Int32, ParameterDirection.Input);
            _dynamicParameters.Add("@DeviceTypeId", userLog.DeviceId, DbType.Int32, ParameterDirection.Input);
            _dynamicParameters.Add("@ApplicationVersion", userLog.ApplicationVersion, DbType.String, ParameterDirection.Input, 10);
            _dynamicParameters.Add("@DeviceName", userLog.DeviceName, DbType.String, ParameterDirection.Input, 30);
            _dynamicParameters.Add("@DeviceVersion", userLog.DeviceVersion, DbType.String, ParameterDirection.Input, 10);
            _dynamicParameters.Add("@DevicePlatform", userLog.DevicePlatform, DbType.String, ParameterDirection.Input, 10);
            _dynamicParameters.Add("@DeviceLatitude", userLog.DeviceLatitude, DbType.Decimal, ParameterDirection.Input);
            _dynamicParameters.Add("@DeviceLongitude", userLog.DeviceLongitude, DbType.Decimal, ParameterDirection.Input);
            _dynamicParameters.Add("@IP", userLog.IP, DbType.String, ParameterDirection.Input, 20);

            await _apiSession.Connection.ExecuteAsync(_sqlBuilder.ToString(), _dynamicParameters, _apiSession.Transaction);
        }
        #endregion

        #region Insert User Herd
        public async Task<bool> InsertUserHerd(IEnumerable<UserHerdModel> userHerds)
        {
            var rows = await _apiSession.Connection.InsertAsync(userHerds, transaction: _apiSession.Transaction);

            if (rows > 0)
                return true;
            else
                return false;
        }
        #endregion

        #endregion

        #region UPDATE

        #region Forgot Password
        public async Task<Guid> ForgotPassword(int userId)
        {
            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            var token = Guid.NewGuid();

            _sqlBuilder.AppendLine(" UPDATE Users SET ");
            _sqlBuilder.AppendLine(" Token = @Token, ");
            _sqlBuilder.AppendLine(" TokenDate = GETDATE(), ");
            _sqlBuilder.AppendLine(" LastModifiedDateTime = GETDATE() ");
            _sqlBuilder.AppendLine(" WHERE Id = @Id ");

            _dynamicParameters.Add("@Id", userId, DbType.Int32, ParameterDirection.Input);
            _dynamicParameters.Add("@Token", token, DbType.Guid, ParameterDirection.Input);

            await _apiSession.Connection.ExecuteAsync(_sqlBuilder.ToString(), _dynamicParameters, _apiSession.Transaction);

            return token;
        }
        #endregion

        #region Update User
        public async Task UpdateUser(UserModel user)
        {
            ValidateModelState(user);

            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" UPDATE Users ");
            _sqlBuilder.AppendLine(" SET ");
            _sqlBuilder.AppendLine(" UserName = @UserName, ");
            _sqlBuilder.AppendLine(" FirstName  = @FirstName, ");
            _sqlBuilder.AppendLine(" LastName  = @LastName, ");
            _sqlBuilder.AppendLine(" PhoneNumber = @PhoneNumber, ");
            _sqlBuilder.AppendLine(" UserRoleId = @UserRoleId, ");
            _sqlBuilder.AppendLine(" Active = @Active, ");
            _sqlBuilder.AppendLine(" ActiveDate = @ActiveDate, ");
            _sqlBuilder.AppendLine(" InactiveDate = @InactiveDate, ");
            _sqlBuilder.AppendLine(" LastModifiedDateTime = GETDATE() ");
            _sqlBuilder.AppendLine(" WHERE Id = @Id; ");

            _dynamicParameters.Add("@UserName", user.UserName, DbType.String, ParameterDirection.Input, 70);
            _dynamicParameters.Add("@FirstName", user.FirstName, DbType.String, ParameterDirection.Input, 60);
            _dynamicParameters.Add("@LastName", user.LastName, DbType.String, ParameterDirection.Input, 60);
            _dynamicParameters.Add("@PhoneNumber", user.PhoneNumber, DbType.String, ParameterDirection.Input, 55);
            _dynamicParameters.Add("@UserRoleId", user.UserRoleId, DbType.Int32, ParameterDirection.Input);
            _dynamicParameters.Add("@Active", user.Active, DbType.Boolean, ParameterDirection.Input);
            _dynamicParameters.Add("@ActiveDate", user.ActiveDate, DbType.DateTime, ParameterDirection.Input);
            _dynamicParameters.Add("@InactiveDate", user.InactiveDate, DbType.DateTime, ParameterDirection.Input);
            _dynamicParameters.Add("@Id", user.Id, DbType.Int32, ParameterDirection.Input);

            await _apiSession.Connection.ExecuteScalarAsync<int>(_sqlBuilder.ToString(), _dynamicParameters, _apiSession.Transaction);
        }
        #endregion

        #region Register User
        public async Task RegisterUser(int userId, string password)
        {
            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            var salt = Guid.NewGuid().ToString().ToUpper();

            _sqlBuilder.AppendLine(" UPDATE Users  ");
            _sqlBuilder.AppendLine(" SET PasswordHash = HASHBYTES('SHA2_512', @Password + CAST(@Salt AS NVARCHAR(36))) ");
            _sqlBuilder.AppendLine("   , Salt = @Salt ");
            _sqlBuilder.AppendLine("   , Token = null ");
            _sqlBuilder.AppendLine("   , TokenDate = null ");
            _sqlBuilder.AppendLine(" WHERE Id = @UserId ");

            _dynamicParameters.Add("@Password", password, DbType.String, ParameterDirection.Input, 45);
            _dynamicParameters.Add("@Salt", salt, DbType.String, ParameterDirection.Input, 36);
            _dynamicParameters.Add("@UserId", userId, DbType.Int32, ParameterDirection.Input);

            await _apiSession.Connection.ExecuteScalarAsync<int>(_sqlBuilder.ToString(), param: _dynamicParameters, _apiSession.Transaction);
        }
        #endregion

        #region Change Password
        public async Task ChangePassword(int userId, string password)
        {
            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            var salt = Guid.NewGuid().ToString().ToUpper();

            _sqlBuilder.AppendLine(" UPDATE Users ");
            _sqlBuilder.AppendLine("    SET PasswordHash = HASHBYTES('SHA2_512', @Password + CAST(@Salt AS NVARCHAR(36))) ");
            _sqlBuilder.AppendLine("  , Salt = @Salt ");
            _sqlBuilder.AppendLine("  , LastModifiedDateTime = GETDATE()   ");
            _sqlBuilder.AppendLine("  , Token = Null ");
            _sqlBuilder.AppendLine("  , TokenDate = null ");
            _sqlBuilder.AppendLine(" WHERE Id = @Id ");

            _dynamicParameters.Add("@Password", password, DbType.String, ParameterDirection.Input, 45);
            _dynamicParameters.Add("@Salt", salt, DbType.String, ParameterDirection.Input, 36);
            _dynamicParameters.Add("@Id", userId, DbType.Int32, ParameterDirection.Input);

            await _apiSession.Connection.ExecuteAsync(_sqlBuilder.ToString(), param: _dynamicParameters, transaction: _apiSession.Transaction);
        }
        #endregion

        #endregion

        #region GET

        #region Get User By Email
        public async Task<UserModel> GetUserByEmail(string email)
        {
            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" SELECT TOP 1 Id, UserRoleId, UserName, FirstName, LastName, PhoneNumber, Token, Active, TokenDate, ActiveDate, InactiveDate, CreatedDateTime, LastModifiedDateTime FROM Users ");
            _sqlBuilder.AppendLine(" WHERE UserName = @UserName ");

            _dynamicParameters.Add("@UserName", email, DbType.String, ParameterDirection.Input, 70);

            return await _apiSession.Connection.QueryFirstOrDefaultAsync<UserModel>(_sqlBuilder.ToString(), param: _dynamicParameters, transaction: _apiSession.Transaction);
        }
        #endregion

        #region Get All Users
        public async Task<IEnumerable<UserModel>> GetAllUsers(string email, string firstName, string lastName, string phoneNumber, FilterActiveEnum filterActive)
        {
            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" SELECT Id ");
            _sqlBuilder.AppendLine("    ,UserRoleId ");
            _sqlBuilder.AppendLine("    ,UserName ");
            _sqlBuilder.AppendLine("    ,FirstName ");
            _sqlBuilder.AppendLine("    ,LastName ");
            _sqlBuilder.AppendLine("    ,PhoneNumber ");
            _sqlBuilder.AppendLine("    ,Active ");
            _sqlBuilder.AppendLine("    ,ActiveDate ");
            _sqlBuilder.AppendLine("    ,InactiveDate  ");
            _sqlBuilder.AppendLine("    ,CASE ");
            _sqlBuilder.AppendLine(" 	      WHEN CONVERT(DATE,COALESCE(TokenDate, GETDATE())) < CONVERT(DATE, GETDATE()) ");
            _sqlBuilder.AppendLine(" 	      THEN 1 ");
            _sqlBuilder.AppendLine(" 	      ELSE ");
            _sqlBuilder.AppendLine(" 	      0 ");
            _sqlBuilder.AppendLine(" 	  END AS CanResendRegisterEmail ");
            _sqlBuilder.AppendLine(" FROM Users  ");
            _sqlBuilder.AppendLine(" WHERE 1 = 1  ");

            if (!string.IsNullOrEmpty(email))
            {
                _sqlBuilder.AppendLine(" AND UserName = @UserName ");
                _dynamicParameters.Add("@UserName", email, DbType.String, ParameterDirection.Input, 70);
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                _sqlBuilder.AppendLine(" AND FirstName = @FirstName ");
                _dynamicParameters.Add("@FirstName", firstName, DbType.String, ParameterDirection.Input, 60);
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                _sqlBuilder.AppendLine(" AND LastName = @LastName ");
                _dynamicParameters.Add("@LastName", lastName, DbType.String, ParameterDirection.Input, 60);
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                _sqlBuilder.AppendLine(" AND PhoneNumber like @PhoneNumber ");
                _dynamicParameters.Add("@PhoneNumber", $"%{phoneNumber.Replace(" ", "")}%", DbType.String, ParameterDirection.Input, 25);
            }

            if (filterActive.Equals(FilterActiveEnum.True))
                _sqlBuilder.AppendLine(" AND Active = 1 ");
            else if (filterActive.Equals(FilterActiveEnum.False))
                _sqlBuilder.AppendLine(" AND Active = 0 ");

            _sqlBuilder.AppendLine(" ORDER BY Id DESC ");

            return (await _apiSession.Connection.QueryAsync<UserModel>(_sqlBuilder.ToString(), param: _dynamicParameters, transaction: _apiSession.Transaction)).ToList();
        }
        #endregion

        #region Get User By Id
        public async Task<UserModel> GetUserById(int id)
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

            _sqlBuilder.AppendLine(" WHERE us.Id = @Id ");

            _dynamicParameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);

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

        #region Get User By Token
        public async Task<UserModel> GetUserByToken(Guid token)
        {
            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" SELECT TOP 1 Id, UserRoleId, UserName, FirstName, LastName, PhoneNumber, TokenDate, Active, ActiveDate, InactiveDate FROM Users");
            _sqlBuilder.AppendLine(" WHERE Token = @Token ");

            _dynamicParameters.Add("@Token", token, DbType.Guid, ParameterDirection.Input, 50);

            return await _apiSession.Connection.QueryFirstOrDefaultAsync<UserModel>(_sqlBuilder.ToString(), param: _dynamicParameters, transaction: _apiSession.Transaction);
        }
        #endregion

        #region Get User Log
        public async Task<IEnumerable<UserLogModel>> GetUserLog()
        {
            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" SELECT UL.Id, UL.UserId, UL.UserLogActionId, UL.DeviceTypeId AS DeviceId, UL.ActionDate, UL.ApplicationVersion, UL.DeviceName, UL.DeviceVersion, UL.DevicePlatform, UL.DeviceLatitude, UL.DeviceLongitude, UL.IP, ");
            _sqlBuilder.AppendLine(" ULA.Id, ULA.Description, ");
            _sqlBuilder.AppendLine(" DT.Id, DT.DeviceTypeName, DT.Active ");
            _sqlBuilder.AppendLine(" FROM UserLog UL ");
            _sqlBuilder.AppendLine(" INNER JOIN UserLogAction ULA ON (UL.UserLogActionId = ULA.Id) ");
            _sqlBuilder.AppendLine(" INNER JOIN DeviceType DT ON (UL.DeviceTypeId = DT.Id) ");
            _sqlBuilder.AppendLine(" WHERE UL.UserId = @Id ");
            _sqlBuilder.AppendLine(" ORDER BY UL.Id DESC ");

            _dynamicParameters.Add("@Id", _claims.UserId, DbType.Int32, ParameterDirection.Input);

            return (await _apiSession.Connection.QueryAsync<UserLogModel, UserLogActionModel, DeviceTypeModel, UserLogModel>(_sqlBuilder.ToString(),
                (userLog, logAction, deviveType) =>
                {
                    if (userLog != null)
                    {
                        userLog.SetDevice(deviveType);
                        userLog.SetUserLogAction(logAction);
                    }

                    return userLog;
                },
                param: _dynamicParameters,
                transaction: _apiSession.Transaction,
                splitOn: "Id, Id"
                )).ToList();
        }
        #endregion

        #region Get User Roles
        public async Task<IEnumerable<UserRoleModel>> GetUserRoles()
        {
            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" SELECT Id AS UserRoleId, ");
            _sqlBuilder.AppendLine("    RoleName AS RoleName ");
            _sqlBuilder.AppendLine(" FROM UserRole ");
            _sqlBuilder.AppendLine(" WHERE Active = 1 ");
            _sqlBuilder.AppendLine(" ORDER BY Id ");

            return (await _apiSession.Connection.QueryAsync<UserRoleModel>(_sqlBuilder.ToString(), param: _dynamicParameters, transaction: _apiSession.Transaction)).ToList();
        }
        #endregion

        #region Get User Roles
        public async Task<UserRoleModel> GetUserRolesById(int userRoleId)
        {
            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" SELECT Id AS UserRoleId, ");
            _sqlBuilder.AppendLine("    RoleName AS RoleName ");
            _sqlBuilder.AppendLine(" FROM UserRole ");
            _sqlBuilder.AppendLine(" WHERE Active = 1 ");
            _sqlBuilder.AppendLine(" AND Id = @Id ");
            _sqlBuilder.AppendLine(" ORDER BY Id ");

            _dynamicParameters.Add("@Id", userRoleId, DbType.Int32, ParameterDirection.Input);

            return await _apiSession.Connection.QueryFirstOrDefaultAsync<UserRoleModel>(_sqlBuilder.ToString(), param: _dynamicParameters, transaction: _apiSession.Transaction);
        }
        #endregion


        #region Get User Log By Id
        public async Task<UserLogModel> GetUserLogById(int userLogId)
        {
            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" SELECT UL.Id, UL.UserId, UL.UserLogActionId, UL.DeviceTypeId AS DeviceId, UL.ActionDate, UL.ApplicationVersion, UL.DeviceName, UL.DeviceVersion, UL.DevicePlatform, UL.DeviceLatitude, UL.DeviceLongitude, UL.IP, ");
            _sqlBuilder.AppendLine(" ULA.Id, ULA.Description, ");
            _sqlBuilder.AppendLine(" DT.Id, DT.DeviceTypeName, DT.Active ");
            _sqlBuilder.AppendLine(" FROM UserLog UL ");
            _sqlBuilder.AppendLine(" INNER JOIN UserLogAction ULA ON (UL.UserLogActionId = ULA.Id) ");
            _sqlBuilder.AppendLine(" INNER JOIN DeviceType DT ON (UL.DeviceTypeId = DT.Id) ");
            _sqlBuilder.AppendLine(" WHERE UL.UserId = @Id ");
            _sqlBuilder.AppendLine(" AND UL.Id = @userLogId ");

            _dynamicParameters.Add("@Id", _claims.UserId, DbType.Int32, ParameterDirection.Input);
            _dynamicParameters.Add("@userLogId", userLogId, DbType.Int32, ParameterDirection.Input);

            return (await _apiSession.Connection.QueryAsync<UserLogModel, UserLogActionModel, DeviceTypeModel, UserLogModel>(_sqlBuilder.ToString(),
               (userLog, logAction, deviveType) =>
               {
                   if (userLog != null)
                   {
                       userLog.SetDevice(deviveType);
                       userLog.SetUserLogAction(logAction);
                   }

                   return userLog;
               },
               param: _dynamicParameters,
               transaction: _apiSession.Transaction,
               splitOn: "Id, Id"
               )).FirstOrDefault();
        }
        #endregion

        #region Is Herd Associated With User
        public async Task<bool> IsHerdAssociatedWithUser(int userId, string herdCode)
        {
            StringBuilder _sqlBuilder = new();

            _sqlBuilder.AppendLine(" SELECT COUNT(*) ");
            _sqlBuilder.AppendLine(" FROM UserHerd ");
            _sqlBuilder.AppendLine(" WHERE UserId = @UserId ");
            _sqlBuilder.AppendLine("   AND HerdCode = @HerdCode ");

            _dynamicParameters = new DynamicParameters();
            _dynamicParameters.Add("@UserId", userId, DbType.Int32, ParameterDirection.Input);
            _dynamicParameters.Add("@HerdCode", herdCode, DbType.String, ParameterDirection.Input, 8);

            return await _apiSession.Connection.ExecuteScalarAsync<bool>(_sqlBuilder.ToString(), param: _dynamicParameters, transaction: _apiSession.Transaction);
        }
        #endregion

        #region Get Users Email
        public async Task<IEnumerable<UserResponseModel>> GetUsersEmail(bool active = true)
        {
            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" SELECT Id, UserName, FirstName, LastName, Active FROM Users");
            _sqlBuilder.AppendLine(" WHERE Active = @Active ");

            _dynamicParameters.Add("@Active", active, DbType.Boolean, ParameterDirection.Input);

            return (await _apiSession.Connection.QueryAsync<UserResponseModel>(_sqlBuilder.ToString(), param: _dynamicParameters, transaction: _apiSession.Transaction)).ToList();
        }
        #endregion

        #region Get User Herds
        public async Task<IEnumerable<string>> GetUserHerds(int userId)
        {
            StringBuilder _sqlBuilder = new();
            _dynamicParameters = new DynamicParameters();

            _sqlBuilder.AppendLine(" SELECT HerdCode ");
            _sqlBuilder.AppendLine(" FROM UserHerd ");
            _sqlBuilder.AppendLine(" WHERE UserId = @UserId ");

            _dynamicParameters.Add("@UserId", userId, DbType.Int32, ParameterDirection.Input);

            return (await _apiSession.Connection.QueryAsync<string>(_sqlBuilder.ToString(), param: _dynamicParameters, transaction: _apiSession.Transaction)).ToList();
        }
        #endregion

        #endregion
    }
}
