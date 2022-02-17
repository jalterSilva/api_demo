using FluentValidation;
using GSP.API.Core.Models.Application;
using GSP.API.Core.Models.Base;

namespace GSP.API.Core.Models.Authentication
{
    public class AuthenticationModel : BaseModel
    {
        #region Properties
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserLogRequestModel UserLog { get; set; }
        public string ClientIP { get; set; }
        public bool RememberMe { get; set; }
        #endregion

        #region Constructor
        public AuthenticationModel(string userName, string password, UserLogRequestModel userLog, string clientIP, bool rememberMe)
        {
            UserName = userName;
            Password = password;
            UserLog = userLog;
            ClientIP = clientIP;
            RememberMe = rememberMe;
            ValidateModel();
        }
        #endregion

        #region Validate Model
        public override void ValidateModel()
        {
            ValidationResult = new AuthenticationValidation().Validate(this);
        } 
        #endregion

        #region Fluent Validation
        public class AuthenticationValidation : AbstractValidator<AuthenticationModel>
        {
            public AuthenticationValidation()
            {
                RuleFor(x => x.UserName).NotEmpty().When(x => string.IsNullOrEmpty(x.UserName)).WithMessage("Invalid Username");

                RuleFor(x => x.Password).NotEmpty().When(x => string.IsNullOrEmpty(x.Password)).WithMessage("Invalid Password");

                RuleFor(x => x.UserLog).NotNull().WithMessage("{PropertyName} cannot be empty");
            }
        }
        #endregion

    }
}
