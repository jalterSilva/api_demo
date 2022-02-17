using FluentValidation;
using GSP.API.Core.Models.Base;

namespace GSP.API.Core.Models.User
{
    public class RecoverRegisterModel : BaseModel
    {
        #region Properties
        public string Token { get; set; }
        #endregion

        #region Constructor
        public RecoverRegisterModel()
        {
            ValidateModel();
        }

        public RecoverRegisterModel(string token)
        {
            SetToken(token);
            ValidateModel();
        }
        #endregion

        #region Validate Model
        public override void ValidateModel()
        {
            ValidationResult = new RecoverRegisterValidation().Validate(this);
        }
        #endregion

        #region Methods
        public void SetToken(string token)
        {
            Token = token;
            ValidateModel();
        }
        #endregion

        #region Fluent Validation
        public class RecoverRegisterValidation : AbstractValidator<RecoverRegisterModel>
        {
            public RecoverRegisterValidation()
            {
                RuleFor(x => x.Token).NotEmpty().When(x => string.IsNullOrEmpty(x.Token)).WithMessage("{PropertyName} is required");
            }
        }
        #endregion
    }
}
