using GSP.API.Core.Models.Application;

namespace GSP.API.Core.Models.User
{
    public class RegisterUserRequestModel
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public UserLogRequestModel UserLog { get; set; }
    }
}
