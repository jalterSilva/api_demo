using GSP.API.Core.Managers.User;
using GSP.API.Core.Models.Application;
using GSP.API.Core.Models.Enum;
using GSP.API.Core.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GSP.API.Application.Controllers.User
{
    [Route("[controller]")]
    [ApiController]
    [Produces(typeof(ResultModel))]
    public class UserController : BaseController
    {
        #region Properties
        private readonly IUserManager _userManager;
        #endregion

        #region Constructor
        public UserController([FromServices] IConfiguration configuration,
                              [FromServices] IWebHostEnvironment environment,
                              [FromServices] IHttpContextAccessor httpContextAccessor,
                              IUserManager userManager
                              )
           : base(configuration, environment, httpContextAccessor)
        {
            _userManager = userManager;
        }
        #endregion

        #region POST - Create a new user
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="requestModel">User request model</param>
        /// <returns></returns>
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<IActionResult> CreateNewUser([FromBody] AddOrUpdateUserRequestModel requestModel)
            => Ok(await _userManager.CreateNewUser(requestModel));
        #endregion

        #region PUT - Update user
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="requestModel">requestModel</param>
        /// <returns></returns>
        [HttpPut]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> UpdateUser([FromBody] AddOrUpdateUserRequestModel requestModel)
            => Ok(await _userManager.UpdateUser(requestModel));
        #endregion

        #region PUT - Change Password
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="requestModel">Request Model</param>
        /// <returns></returns>
        [HttpPut]
        [ApiExplorerSettings(IgnoreApi = true)]
        [AllowAnonymous]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordRequestModel requestModel)
            => Ok(await _userManager.ChangePassword(requestModel));
        #endregion

        #region PUT - Change User Status
        /// <summary>
        /// Change User Status
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("RecoverRegister/{userId}/{status}")]
        public async Task<IActionResult> ChangeUserStatus([FromRoute] int userId, [FromRoute] bool status)
            => Ok(await _userManager.ChangeUserStatus(userId, status));
        #endregion

        #region GET - Get all users
        /// <summary>
        /// Get all users
        /// </summary>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="filterActive"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUser([FromQuery] string email, [FromQuery] string firstName, [FromQuery] string lastName, [FromQuery] string phoneNumber, [FromQuery] FilterActiveEnum filterActive)
            => Ok(await _userManager.GetAllUsers(email, firstName, lastName, phoneNumber, filterActive));
        #endregion

        #region GET - Get user by id
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
            => Ok(await _userManager.GetUserById(id));
        #endregion

        #region GET - Get User Log 
        /// <summary>
        /// Get user log
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("UserLog")]
        public async Task<IActionResult> GetUserLog()
            => Ok(await _userManager.GetUserLog());
        #endregion

        #region GET - Get User Log 
        /// <summary>
        /// Get User Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles()
            => Ok(await _userManager.GetUserRoles());
        #endregion

        #region GET - Get User Log By Id
        /// <summary>
        /// Get User Log By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("UserLog/{userLogId}")]
        public async Task<IActionResult> GetUserLogById([FromRoute] int userLogId)
            => Ok(await _userManager.GetUserLogById(userLogId));
        #endregion

        #region GET - Get all users
        /// <summary>
        /// Get all users
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUsersEmail/{active}")]
        public async Task<IActionResult> GetUsersEmail([FromRoute] bool active = true)
            => Ok(await _userManager.GetUsersEmail(active));
        #endregion

    }
}
