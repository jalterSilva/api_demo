using GSP.API.Core.Models.Application;

namespace GSP.API.Core.Models.User
{
    public class ChangeUserPasswordRequestModel
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public UserLogRequestModel UserLog { get; set; }
        public string Language { get; set; }
    }
}
