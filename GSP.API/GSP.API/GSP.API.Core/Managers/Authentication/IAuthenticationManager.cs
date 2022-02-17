using GSP.API.Core.Models.Application;
using GSP.API.Core.Models.Authentication;
using System.Threading.Tasks;

namespace GSP.API.Core.Managers.Authentication
{
    public interface IAuthenticationManager
    {
        /// <summary>
        /// Login and Get JWT Token
        /// </summary>
        /// <param name="authenticationRequest"></param>
        /// <returns></returns>
        Task<ResultModel> Login(AuthenticationRequestModel authenticationRequest);
    }
}
