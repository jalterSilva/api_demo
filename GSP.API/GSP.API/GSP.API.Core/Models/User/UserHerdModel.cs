using Dapper.Contrib.Extensions;
using FluentValidation;
using GSP.API.Core.Models.Base;
using Newtonsoft.Json;
using System;

namespace GSP.API.Core.Models.User
{
    [Table("UserHerd")]
    public class UserHerdModel : BaseModel
    {
        #region Properties
        public int UserId { get; private set; }
        public string HerdCode { get; private set; }
        public bool Active { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public int CreatedUserId { get; private set; }
        public DateTime LastModifiedDateTime { get; private set; }
        public int LastModifiedUserId { get; private set; }
        #endregion

        #region Constructor
        public UserHerdModel()
        {
            ValidateModel();
        }

        public UserHerdModel(int userId, string herdCode, int createdUserId)
        {
            SetUserId(userId);
            SetHerdCode(herdCode);
            SetActive();
            SetCreatedUserId(createdUserId);
            SetLastModifiedUserId(createdUserId);
            ValidateModel();
        }

        [JsonConstructor]
        public UserHerdModel(int userId, string herdCode, bool active, DateTime createdDateTime, int createdUserId, DateTime lastModifiedDateTime, int lastModifiedUserId)
        {
            SetUserId(userId);
            SetHerdCode(herdCode);
            Active = active;
            CreatedDateTime = createdDateTime;
            CreatedUserId = createdUserId;
            LastModifiedDateTime = lastModifiedDateTime;
            LastModifiedUserId = lastModifiedUserId;
            ValidateModel();
        }
        #endregion

        #region Validate Model
        public override void ValidateModel()
        {
            ValidationResult = new UserHerdValidation().Validate(this);
        } 
        #endregion

        #region Methods
        public void SetUserId(int userId)
        {
            UserId = userId;
            ValidateModel();
        }

        public void SetHerdCode(string herdCode)
        {
            HerdCode = herdCode;
            ValidateModel();
        }

        public void SetActive() =>  Active = true;

        public void SetInactive() =>  Active = false;

        public void SetCreatedUserId(int createdUserId)
        {
            CreatedDateTime = DateTime.Now;
            CreatedUserId = createdUserId;
            ValidateModel();
        }

        public void SetLastModifiedUserId(int lastModifiedUserId)
        {
            LastModifiedDateTime = DateTime.Now;
            LastModifiedUserId = lastModifiedUserId;
            ValidateModel();
        }
        #endregion

        #region Fluent Validation
        public class UserHerdValidation : AbstractValidator<UserHerdModel>
        {
            public UserHerdValidation()
            {
                RuleFor(x => x.UserId).Must(x => x > 0).WithMessage("{PropertyName} cannot be 0");
                RuleFor(x => x.HerdCode).NotEmpty().When(x => string.IsNullOrEmpty(x.HerdCode)).WithMessage("{PropertyName} cannot be empty");
                RuleFor(x => x.CreatedUserId).Must(x => x > 0).WithMessage("{PropertyName} cannot be 0");
                RuleFor(x => x.LastModifiedUserId).Must(x => x > 0).WithMessage("{PropertyName} cannot be 0");

            }
        }
        #endregion
    }
}
