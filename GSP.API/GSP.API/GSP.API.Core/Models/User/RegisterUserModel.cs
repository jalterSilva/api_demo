using FluentValidation;
using GSP.API.Core.Models.Application;
using GSP.API.Core.Models.Base;

namespace GSP.API.Core.Models.User
{
    public class RegisterUserModel : BaseModel
    {
        #region Properties
        public int UserId { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public UserLogRequestModel UserLog { get; set; }
        #endregion

        #region Constructor
        public RegisterUserModel() 
        {
            ValidateModel();
        }

        public RegisterUserModel(int userId, string token, string password, UserLogRequestModel userLog)
        {
            SetUserId(userId);
            SetToken(token);
            SetPassword(password);
            SetUserLog(userLog);
            ValidateModel();
        }
        #endregion

        #region Validate Model
        public override void ValidateModel()
        {
            ValidationResult = new RegisterUserRequestValidation().Validate(this);
        } 
        #endregion

        #region Methods
        public void SetUserId(int userId) => this.UserId = userId;

        public void SetToken(string token)
        {
            Token = token;
            ValidateModel();
        }

        public void SetPassword(string password)
        {
            Password = password;
            ValidateModel();
        }

        public void SetUserLog(UserLogRequestModel userLog)
        {
            UserLog = userLog;
            ValidateModel();
        }
        #endregion

        #region Fluent Validation
        public class RegisterUserRequestValidation : AbstractValidator<RegisterUserModel>
        {
            public RegisterUserRequestValidation()
            {
                RuleFor(x => x.Token).NotEmpty().When(x => string.IsNullOrEmpty(x.Token)).WithMessage("{PropertyName} is required");
                RuleFor(x => x.Password).NotEmpty().When(x => string.IsNullOrEmpty(x.Password)).WithMessage("{PropertyName} is required");
            }
        }
        #endregion

    }
}
