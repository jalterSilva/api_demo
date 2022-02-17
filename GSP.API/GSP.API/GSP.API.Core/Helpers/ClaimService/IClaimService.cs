using GSP.API.Core.Models.Authentication;
using GSP.API.Core.Models.User;

namespace GSP.API.Core.Helpers.ClaimService
{
    public interface IClaimService
    {
        /// <summary>
        /// Set token and claims for user
        /// </summary>
        /// <param name="user">User object to set claim</param>
        /// <param name="herd">User object to set claim</param>
        /// <returns></returns>
        TokenResponseModel CreateToken(UserModel user);
    }
}
