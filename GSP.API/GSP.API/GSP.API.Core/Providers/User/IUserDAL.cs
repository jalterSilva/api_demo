using GSP.API.Core.Models.Enum;
using GSP.API.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSP.API.Core.Providers.User
{
    public interface IUserDAL
    {
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<UserModel> CreateNewUser(UserModel user, string password);

        /// <summary>
        /// Add User Log Action
        /// </summary>
        /// <param name="userLog"></param>
        /// <returns></returns>
        Task AddUserLogAction(UserLogModel userLog);

        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <returns></returns>
        Task<Guid> ForgotPassword(int userId);

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<UserModel> GetUserByEmail(string email);

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="filterActive"></param>
        /// <returns></returns>
        Task<IEnumerable<UserModel>> GetAllUsers(string email, string firstName, string lastName, string phoneNumber, FilterActiveEnum filterActive);

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserModel> GetUserById(int id);

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task UpdateUser(UserModel user);

        /// <summary>
        /// Get User By Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<UserModel> GetUserByToken(Guid token);

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task RegisterUser(int userId, string password);

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task ChangePassword(int userId, string password);

        /// <summary>
        /// Get User Log
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UserLogModel>> GetUserLog();

        /// <summary>
        /// Get User Roles
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UserRoleModel>> GetUserRoles();

        /// <summary>
        /// Get User Roles By Id
        /// </summary>
        /// <returns></returns>
        Task<UserRoleModel> GetUserRolesById(int userRoleId);

        /// <summary>
        /// Get User Log By Id
        /// </summary>
        /// <param name="userLogId"></param>
        /// <returns></returns>
        Task<UserLogModel> GetUserLogById(int userLogId);

        /// <summary>
        /// Insert User Log
        /// </summary>
        /// <param name="userLog"></param>
        /// <returns></returns>
        Task InsertUserLog(UserLogModel userLog);

        /// <summary>
        /// Get Users Email
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        Task<IEnumerable<UserResponseModel>> GetUsersEmail(bool active = true);

        /// <summary>
        /// Get User Herds
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetUserHerds(int userId);
  
    }
}
