using FluentValidation;
using GSP.API.Core.Models.Base;
using Newtonsoft.Json;
using System;

namespace GSP.API.Core.Models.User
{
    public class UserModel : BaseModel
    {
        #region Properties
        public static int Max_Length_User_Name = 70;
        public static int Max_Length_First_Last_Name = 60;
        public static int Max_Length_Phone_Number_Name = 25;

        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhoneNumber { get; private set; }
        public Guid? Token { get; private set; }
        public DateTime? TokenDate { get; private set; }
        public bool Active { get; private set; }
        public DateTime? ActiveDate { get; private set; }
        public DateTime? InactiveDate { get; private set; }
        public string CurrentHerdCode { get; set; }
        public int UserRoleId { get; private set; }
        public UserRoleModel UserRole { get; set; } = new UserRoleModel();
        public bool CanResendRegisterEmail { get; set; }
        #endregion

        #region Constructor
        public UserModel(string userName, string firstName, string lastName, string phoneNumber, int userRoleId)
        {

            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            SetActive();
            UserRoleId = userRoleId;
            ValidateModel();
        }

        [JsonConstructor]
        public UserModel(int id, int userRoleId, string userName, string firstName, string lastName, string phoneNumber, Guid? token, DateTime? tokenDate, bool active, DateTime? activeDate, DateTime? inactiveDate)
        {
            SetId(id);
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Token = token;
            TokenDate = tokenDate;
            Active = active;
            ActiveDate = activeDate;
            InactiveDate = inactiveDate;
            SetUserRoleId(userRoleId);
            ValidateModel();
        }

        public UserModel()
        {
            ValidateModel();
        }

        #endregion

        #region Validate Model
        public override void ValidateModel()
        {
            ValidationResult = new UserValidation().Validate(this);
        }
        #endregion

        #region Methods

        public void SetUserRoleId(int userRoleId)
        {
            UserRoleId = userRoleId;
            ValidateModel();

        }
        public void SetUserName(string userName)
        {
            UserName = userName;
            ValidateModel();
        }

        public void SetFirstName(string firstName)
        {
            FirstName = firstName;
            ValidateModel();
        }

        public void SetLastName(string lastName)
        {
            LastName = lastName;
            ValidateModel();
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            ValidateModel();
        }

        public void SetToken(Guid token)
        {
            Token = token;
            TokenDate = DateTime.Now;
        }

        public void SetActive()
        {
            Active = true;
            ActiveDate = DateTime.Now;
        }

        public void SetInactive()
        {
            Active = false;
            InactiveDate = DateTime.Now;
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}"?.Trim();
        }

        public bool IsUserActived() => this.Active == true;
        #endregion

        #region Fluent Validation
        public class UserValidation : AbstractValidator<UserModel>
        {
            public UserValidation()
            {
                RuleFor(x => x.UserRoleId).GreaterThan(0).WithMessage("User Role is required");

                RuleFor(x => x.UserName).NotEmpty().When(x => string.IsNullOrEmpty(x.UserName)).WithMessage("{PropertyName} cannot be empty");
                RuleFor(x => x.UserName).MaximumLength(Max_Length_User_Name).WithMessage($"{{PropertyName}} lenght cannot by greater than {Max_Length_User_Name}");

                RuleFor(x => x.FirstName).NotEmpty().When(x => string.IsNullOrEmpty(x.FirstName)).WithMessage("{PropertyName} cannot be empty");
                RuleFor(x => x.FirstName).MaximumLength(Max_Length_First_Last_Name).WithMessage($"{{PropertyName}} lenght cannot by greater than {Max_Length_First_Last_Name}");

                RuleFor(x => x.LastName).NotEmpty().When(x => string.IsNullOrEmpty(x.LastName)).WithMessage("{PropertyName} cannot be empty");
                RuleFor(x => x.LastName).MaximumLength(Max_Length_First_Last_Name).WithMessage($"{{PropertyName}} lenght cannot by greater than {Max_Length_First_Last_Name}");

                RuleFor(x => x.PhoneNumber).MaximumLength(Max_Length_Phone_Number_Name).WithMessage($"{{PropertyName}} lenght cannot by greater than {Max_Length_Phone_Number_Name}");
            }
        }
        #endregion
    }
}
