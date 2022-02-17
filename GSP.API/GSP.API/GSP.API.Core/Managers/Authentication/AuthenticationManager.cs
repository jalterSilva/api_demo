using AutoMapper;
using GSP.API.Core.Helpers.ClaimService;
using GSP.API.Core.Managers.Base;
using GSP.API.Core.Models.Application;
using GSP.API.Core.Models.Authentication;
using GSP.API.Core.Models.System;
using GSP.API.Core.Models.User;
using GSP.API.Core.Providers.Authentication;
using GSP.API.Core.Providers.User;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GSP.API.Core.Managers.Authentication
{
    public class AuthenticationManager : BaseManager, IAuthenticationManager
    {
        #region Properties
        private readonly IClaimService _claimService;
        private readonly IAuthenticationDAL _authenticationDAL;
        private readonly IUserDAL _userDAL;
        #endregion

        #region Constructor
        public AuthenticationManager(IClaimService claimService, 
                                     IAuthenticationDAL authenticationDAL, 
                                     IUserDAL userDAL,
                                     IMapper mapper,
                                     IHttpContextAccessor httpContextAccessor) 
            : base(mapper, httpContextAccessor)
        {
            _claimService = claimService;
            _authenticationDAL = authenticationDAL;
            _userDAL = userDAL;
        }
        #endregion

        #region GET

        #region Login
        public async Task<ResultModel> Login(AuthenticationRequestModel authenticationRequest)
        {
       
            var authentication = _Mapper.Map<AuthenticationModel>(authenticationRequest);

            ValidateModelState(authentication);

            var user = await _authenticationDAL.ValidateLogin(authentication);
            if (user == null)
                return new ResultModel(new ErrorModel("Invalid User or Password"));

            if (user.Token != default)
                return new ResultModel(new ErrorModel("Expired token, the token is only valid for 24 hours"));

            if (!user.IsUserActived())
                return new ResultModel(new ErrorModel("User inactived"));

            var userLog = new UserLogModel(authentication.UserLog);
            userLog.SetLoginAction();
            userLog.SetUserId(user.Id);

            await _userDAL.AddUserLogAction(userLog);

            var result = _claimService.CreateToken(user);

            return new ResultModel(result);
        }
        #endregion

        #endregion
    }
}
