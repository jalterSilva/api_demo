using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GSP.API.Application.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        #region Properties
        public readonly IConfiguration _configuration;
        public readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Contructor
        public BaseController([FromServices] IConfiguration configuration,
                              [FromServices] IWebHostEnvironment environment,
                              [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion
    }
}
