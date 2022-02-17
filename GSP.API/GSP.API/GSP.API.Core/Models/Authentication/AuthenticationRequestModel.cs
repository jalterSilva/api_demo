using GSP.API.Core.Models.Application;

namespace GSP.API.Core.Models.Authentication
{
    public class AuthenticationRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserLogRequestModel UserLog { get; set; }
        public string ClientIP { get; set; }
        public bool RememberMe { get; set; }
    }
}
