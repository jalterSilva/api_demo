using GSP.API.Core.Models.Application;

namespace GSP.API.Core.Models.User
{
    public class ForgotPasswordModel
    {
        public string EmailAddress { get; set; }

        public UserLogRequestModel UserLog { get; set; }

        public string ClientIP { get; set; }
        public string Language { get; set; }
    }
}
