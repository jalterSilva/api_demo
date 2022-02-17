using FluentValidation;
using GSP.API.Core.Models.Application;
using GSP.API.Core.Models.Base;

namespace GSP.API.Core.Models.User
{
    public class ChangeUserPasswordModel : BaseModel
    {
        #region Properties
        public int UserId { get; private set; }
        public string Password { get; private set; }
        public UserLogRequestModel UserLog { get; private set; }
        #endregion

        #region Constructor
        public ChangeUserPasswordModel(int userId, string password, UserLogRequestModel userLog)
        {
            SetUserId(userId);
            SetPassword(password);
            SetUserLog(userLog);
            ValidateModel();
        }

        public ChangeUserPasswordModel()
        {
            ValidateModel();
        }
        #endregion

        #region Validate Model
        public override void ValidateModel()
        {
            ValidationResult = new ChangeUserPasswordValidation().Validate(this);
        } 
        #endregion

        #region Methods
        public void SetUserId(int userId) => this.UserId = userId;

        public void SetPassword(string password)
        {
            Password = password;
            ValidateModel();
        }

        public void SetUserLog(UserLogRequestModel userLog)
        {
            this.UserLog = userLog;
            ValidateModel();
        }
        #endregion

        #region Fluent Validation
        public class ChangeUserPasswordValidation : AbstractValidator<ChangeUserPasswordModel>
        {
            public ChangeUserPasswordValidation()
            {
                RuleFor(x => x.Password).NotEmpty().When(x => string.IsNullOrEmpty(x.Password)).WithMessage("{PropertyName} is required");
            }
        }
        #endregion
    }
}
