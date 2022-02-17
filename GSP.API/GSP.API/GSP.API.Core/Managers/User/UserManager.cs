using AutoMapper;
using GSP.API.Core.Helpers.ClaimService;
using GSP.API.Core.Managers.Base;
using GSP.API.Core.Models.Application;
using GSP.API.Core.Models.Enum;
using GSP.API.Core.Models.System;
using GSP.API.Core.Models.User;
using GSP.API.Core.Providers.UnitOfWork;
using GSP.API.Core.Providers.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace GSP.API.Core.Managers.User
{
    public class UserManager : BaseManager, IUserManager
    {
        #region Properties
        private readonly IUserDAL _userDAL;
        private readonly IClaimService _claimService;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public UserManager(IUserDAL userDAL,
                           IClaimService claimService,
                           IUnitOfWork unitOfWork,
                           IMapper mapper,
                           IHttpContextAccessor httpContextAccessor)
            : base(mapper, httpContextAccessor)
        {
            _userDAL = userDAL;
            _claimService = claimService;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region CREATE

        #region Create New User
        public async Task<ResultModel> CreateNewUser(AddOrUpdateUserRequestModel requestModel)
        {
            try
            {

                var user = await _userDAL.GetUserByEmail(requestModel.UserName);
                if (user != null)
                    return new ResultModel(new ErrorModel("", "Already exists a user with this email."));

                var newUser = _Mapper.Map<UserModel>(requestModel);
                ValidateModelState(newUser); // Validate new user

                var userLog = _Mapper.Map<UserLogModel>(requestModel.UserLog);
                ValidateModelState(userLog); // Validate user log

                var password = $"GSP_{new Random().Next(1000000, 9999999)}";

                _unitOfWork.BeginTransaction();
                var insertedUser = await _userDAL.CreateNewUser(newUser, password);
                // User Log
                userLog.SetCreatedUserAction();
                userLog.SetUserId(insertedUser.Id);
                await _userDAL.InsertUserLog(userLog);
                _unitOfWork.Commit();


                return new ResultModel(true);
            }
            catch (CustomException ce)
            {
                throw new CustomException(ce.ResultModel);
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw new CustomException(new ResultModel(new ErrorModel("Error trying to create a new user")));
            }
        }
        #endregion

        #endregion

        #region UPDATE

        #region Forgot Password
        public async Task<ResultModel> ForgotPassword(ForgotPasswordModel requestModel)
        {
            try
            {
                var userLog = _Mapper.Map<UserLogModel>(requestModel.UserLog);
                ValidateModelState(userLog);

                var user = await _userDAL.GetUserByEmail(requestModel.EmailAddress);
                if (user == null)
                    return new ResultModel(new ErrorModel("Email not found."));

                if (!user.IsUserActived())
                    return new ResultModel(new ErrorModel("This user is inactive"));

                _unitOfWork.BeginTransaction();
                var token = await _userDAL.ForgotPassword(user.Id);
                user.SetToken(token);

                // User log
                userLog.SetForgotPasswordAction();
                userLog.SetUserId(user.Id);
                await _userDAL.InsertUserLog(userLog);
                _unitOfWork.Commit();

                return new ResultModel(true);
            }
            catch (CustomException ce)
            {
                throw new CustomException(ce.ResultModel);
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw new CustomException(new ResultModel(new ErrorModel("Error trying to recover password")));
            }
        }
        #endregion

        #region Update User
        public async Task<ResultModel> UpdateUser(AddOrUpdateUserRequestModel requestModel)
        {
            if (requestModel.Id < 1)
                return new ResultModel(new ErrorModel("User not found"));

            var user = await _userDAL.GetUserById((int)requestModel.Id);
            if (user == null)
                return new ResultModel(new ErrorModel("User not found"));

            user.SetFirstName(requestModel.FirstName);
            user.SetLastName(requestModel.LastName);
            user.SetPhoneNumber(requestModel.PhoneNumber);
            user.SetUserName(requestModel.UserName);
            user.SetUserRoleId(requestModel.UserRoleId);

            ValidateModelState(user);

            await _userDAL.UpdateUser(user);

            return new ResultModel(true);
        }
        #endregion

        #region Register User
        public async Task<ResultModel> RegisterUser(RegisterUserRequestModel requestModel)
        {
            try
            {
                var registerUser = _Mapper.Map<RegisterUserModel>(requestModel);
                ValidateModelState(registerUser);

                var userLog = _Mapper.Map<UserLogModel>(requestModel.UserLog);
                ValidateModelState(userLog);

                if (string.IsNullOrEmpty(registerUser.Token))
                    return new ResultModel(new ErrorModel("User not found"));

                if (string.IsNullOrEmpty(registerUser.Password))
                    return new ResultModel(new ErrorModel("Password is required"));

                var user = await _userDAL.GetUserByToken(Guid.Parse(registerUser.Token));
                if (user == null)
                    return new ResultModel(new ErrorModel("User not found"));

                if (user.TokenDate.HasValue && user.TokenDate.Value.Date < DateTime.Now.Date)
                    return new ResultModel(new ErrorModel("Expired token, the token is only valid for 24 hours"));

                if (user.ActiveDate != null)
                    return new ResultModel(new ErrorModel("This user is already registered"));

                if (user.Id != registerUser.UserId)
                    return new ResultModel(new ErrorModel("User not found"));

                _unitOfWork.BeginTransaction();
                await _userDAL.RegisterUser(user.Id, registerUser.Password);
                // User log
                userLog.SetUserRegistered();
                userLog.SetUserId(user.Id);
                await _userDAL.InsertUserLog(userLog);
                _unitOfWork.Commit();

                var result = _Mapper.Map<UserResponseModel>(user);

                return new ResultModel(result);
            }
            catch (CustomException ce)
            {
                throw new CustomException(ce.ResultModel);
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw new CustomException(new ResultModel(new ErrorModel("Error trying to register user")));
            }
        }
        #endregion

        #region Change Password
        public async Task<ResultModel> ChangePassword(ChangeUserPasswordRequestModel requestModel)
        {
            try
            {
                var changePassword = _Mapper.Map<ChangeUserPasswordModel>(requestModel);
                ValidateModelState(changePassword);

                var userLog = _Mapper.Map<UserLogModel>(requestModel.UserLog);
                ValidateModelState(userLog);

                var user = await _userDAL.GetUserById(changePassword.UserId);
                if (user == null)
                    return new ResultModel(new ErrorModel("User not found"));

                _unitOfWork.BeginTransaction();
                await _userDAL.ChangePassword(changePassword.UserId, changePassword.Password);

                // User log
                userLog.SetChangedPasswordAction();
                userLog.SetUserId(user.Id);
                await _userDAL.InsertUserLog(userLog);
                _unitOfWork.Commit();

                return new ResultModel(true);
            }
            catch (CustomException ce)
            {
                throw new CustomException(ce.ResultModel);
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw new CustomException(new ResultModel(new ErrorModel("Error trying to change password")));
            }
        }
        #endregion

        #region Set User Current Herd Code
        public async Task<ResultModel> SetUserCurrentHerdCode(int userId, string herdCode)
        {
            var user = await _userDAL.GetUserById(userId);
            if (user == null)
                return new ResultModel(new ErrorModel("Email not found."));

            if (string.IsNullOrEmpty(herdCode))
                return new ResultModel(new ErrorModel("Herd Code cannot be empty."));

            user.CurrentHerdCode = herdCode;

            return new ResultModel(_claimService.CreateToken(user));
        }
        #endregion

        #region Change User Status
        public async Task<ResultModel> ChangeUserStatus(int userId, bool status)
        {
            if (userId < 1)
                return new ResultModel(new ErrorModel("User not found"));

            var result = await _userDAL.GetUserById(userId);
            var user = new UserModel(result.Id, result.UserRoleId, result.UserName, result.FirstName, result.LastName, result.PhoneNumber, result.Token, result.TokenDate, result.Active, result.ActiveDate, result.InactiveDate);

            if (user == null)
                return new ResultModel(new ErrorModel("User not found"));

            if (status)
                user.SetActive();
            else
                user.SetInactive();

            //user.ValidateModel();
            await _userDAL.UpdateUser(user);

            return new ResultModel(true);
        }
        #endregion

        #endregion

        #region GET

        #region Get All Users
        public async Task<ResultModel> GetAllUsers(string email, string firstName, string lastName, string phoneNumber, FilterActiveEnum filterActive)
        {
            var user = await _userDAL.GetAllUsers(email, firstName, lastName, phoneNumber, filterActive);

            return new ResultModel(user);
        }
        #endregion

        #region Get User By Id
        public async Task<ResultModel> GetUserById(int id)
        {
            if (id < 1)
                return new ResultModel(new ErrorModel("User not found"));

            var user = await _userDAL.GetUserById(id);
            if (user == null)
                return new ResultModel(new ErrorModel("User not found"));

            return new ResultModel(user);
        }
        #endregion

        #region Recover Register
        public async Task<ResultModel> RecoverRegister(RecoverRegisterRequestModel requestModel)
        {
            var recoverPassword = _Mapper.Map<RecoverRegisterModel>(requestModel);
            ValidateModelState(recoverPassword);

            var user = await _userDAL.GetUserByToken(Guid.Parse(recoverPassword.Token));
            if (user == null)
                return new ResultModel(new ErrorModel("User not found"));

            if (user.ActiveDate != null)
                return new ResultModel(new ErrorModel("This user is already registered"));

            var reponse = _Mapper.Map<UserResponseModel>(user);

            return new ResultModel(reponse);
        }
        #endregion

        #region Recover Password
        public async Task<ResultModel> RecoverPassword(RecoverPasswordRequestModel requestModel)
        {
            var recoverPassword = _Mapper.Map<RecoverPasswordModel>(requestModel);

            ValidateModelState(recoverPassword);

            var user = await _userDAL.GetUserByToken(Guid.Parse(recoverPassword.Token));
            if (user == null)
                return new ResultModel(new ErrorModel("User not found"));

            if (!user.IsUserActived())
                return new ResultModel(new ErrorModel("This user is inactive"));

            var result = _Mapper.Map<UserResponseModel>(user);

            return new ResultModel(result);
        }
        #endregion

        #region Get User Log
        public async Task<ResultModel> GetUserLog()
        {
            var result = await _userDAL.GetUserLog();

            return new ResultModel(result);
        }
        #endregion

        #region Get User Roles
        public async Task<ResultModel> GetUserRoles()
        {
            var result = await _userDAL.GetUserRoles();

            return new ResultModel(result);
        }
        #endregion

        #region Get User Log By Id
        public async Task<ResultModel> GetUserLogById(int userLogId)
        {
            if (userLogId < 1)
                return new ResultModel(new ErrorModel("User Log not found"));

            var userLog = await _userDAL.GetUserLogById(userLogId);
            if (userLog == null)
                return new ResultModel(new ErrorModel("User Log not found"));

            return new ResultModel(userLog);
        }
        #endregion

        #region Get Users Email
        public async Task<ResultModel> GetUsersEmail(bool active = true)
        {
            var users = await _userDAL.GetUsersEmail(active);

            return new ResultModel(users);
        }
        #endregion

        #endregion

    }
}
