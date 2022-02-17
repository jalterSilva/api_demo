using GSP.API.Core.Models.Authentication;
using GSP.API.Core.Models.User;
using System.Threading.Tasks;

namespace GSP.API.Core.Providers.Authentication
{
    public interface IAuthenticationDAL
    {
        /// <summary>
        /// Validate Login
        /// </summary>
        /// <param name="authentication"></param>
        /// <returns></returns>
        Task<UserModel> ValidateLogin(AuthenticationModel authentication);
    }
}
