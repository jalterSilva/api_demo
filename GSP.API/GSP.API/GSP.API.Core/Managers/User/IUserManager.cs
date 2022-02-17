using GSP.API.Core.Models.Application;
using GSP.API.Core.Models.Enum;
using GSP.API.Core.Models.User;
using System.Threading.Tasks;

namespace GSP.API.Core.Managers.User
{
    public interface IUserManager
    {
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> CreateNewUser(AddOrUpdateUserRequestModel requestModel);

        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <returns></returns>
        Task<ResultModel> ForgotPassword(ForgotPasswordModel requestModel);

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="filterActive"></param>
        /// <returns></returns>
        Task<ResultModel> GetAllUsers(string email, string firstName, string lastName, string phoneNumber, FilterActiveEnum filterActive);

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel> GetUserById(int id);

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> UpdateUser(AddOrUpdateUserRequestModel requestModel);

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<ResultModel> ChangeUserStatus(int userId, bool status);

        /// <summary>
        /// Get User By Token
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> RecoverRegister(RecoverRegisterRequestModel requestModel);

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> RegisterUser(RegisterUserRequestModel requestModel);

        /// <summary>
        /// Recover Password
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> RecoverPassword(RecoverPasswordRequestModel requestModel);

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> ChangePassword(ChangeUserPasswordRequestModel requestModel);

        /// <summary>
        /// Get User Log
        /// </summary>
        /// <returns></returns>
        Task<ResultModel> GetUserLog();

        /// <summary>
        /// Get User Roles
        /// </summary>
        /// <returns></returns>
        Task<ResultModel> GetUserRoles();
        

        /// <summary>
        /// Get User Log By Id
        /// </summary>
        /// <param name="userLogId"></param>
        /// <returns></returns>
        Task<ResultModel> GetUserLogById(int userLogId);

        /// <summary>
        /// Get Users Email
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        Task<ResultModel> GetUsersEmail(bool active = true);

    }
}
