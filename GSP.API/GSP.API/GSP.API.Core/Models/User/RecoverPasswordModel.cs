using FluentValidation;
using GSP.API.Core.Models.Base;

namespace GSP.API.Core.Models.User
{
    public class RecoverPasswordModel : BaseModel
    {
        #region Properties
        public string Token { get; set; }
        #endregion

        #region Contructor
        public RecoverPasswordModel()
        {
            ValidateModel();
        }

        public RecoverPasswordModel(string token)
        {
            Token = token;
            ValidateModel();
        }
        #endregion

        #region Validate Model
        public override void ValidateModel()
        {
            ValidationResult = new RecoverPasswordValidation().Validate(this);
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
        public class RecoverPasswordValidation : AbstractValidator<RecoverPasswordModel>
        {
            public RecoverPasswordValidation()
            {
                RuleFor(x => x.Token).NotEmpty().When(x => string.IsNullOrEmpty(x.Token)).WithMessage("{PropertyName} is required");
            }
        }
        #endregion
    }
}
