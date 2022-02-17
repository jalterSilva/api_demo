using GSP.API.Core.Managers.Authentication;
using GSP.API.Core.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GSP.API.Application.Controllers.Authentication
{
    [Route("[controller]")]
    //[ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        #region Properties
        private readonly IAuthenticationManager _authenticationManager;
        #endregion

        #region Constructor
        public AuthenticationController([FromServices] IConfiguration configuration,
                                        [FromServices] IWebHostEnvironment environment,
                                        [FromServices] IHttpContextAccessor httpContextAccessor,
                                        IAuthenticationManager authenticationManager)
            : base(configuration, environment, httpContextAccessor)
        {
            _authenticationManager = authenticationManager;
        }
        #endregion

        #region POST - Login
        /// <summary>
        /// Login and Get JWT Token
        /// </summary>
        /// <param name="authenticationRequest">Authentication Request</param>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [Produces(typeof(TokenResponseModel))]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequestModel authenticationRequest)
            => Ok(await _authenticationManager.Login(authenticationRequest));
        #endregion
    }
}
