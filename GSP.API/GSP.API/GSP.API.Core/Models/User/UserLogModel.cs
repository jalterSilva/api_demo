using FluentValidation;
using GSP.API.Core.Models.Application;
using GSP.API.Core.Models.Base;
using GSP.API.Core.Models.Enum;
using Newtonsoft.Json;
using System;

namespace GSP.API.Core.Models.User
{
    public class UserLogModel : BaseModel
    {
        #region Properties
        public int? UserId { get; private set; }
        public UserLogActionEnum UserLogActionId { get; private set; }
        public DeviceTypeEnum DeviceId { get; private set; }
        public DateTime ActionDate { get; private set; }
        public string ApplicationVersion { get; private set; }
        public string DeviceName { get; private set; }
        public string DeviceVersion { get; private set; }
        public string DevicePlatform { get; private set; }
        public double? DeviceLatitude { get; private set; }
        public double? DeviceLongitude { get; private set; }
        public string IP { get; private set; }
        public UserLogActionModel UserLogAction { get; private set; }
        public DeviceTypeModel DeviceType { get; private set; }
        #endregion

        #region Constructor
        public UserLogModel() 
        {
            ValidateModel();
        }

        public UserLogModel(UserLogRequestModel userLogRequest)
        {
            SetApplicationVersion(userLogRequest.ApplicationVersion);
            SetDeviceName(userLogRequest.DeviceName);
            SetDeviceVersion(userLogRequest.DeviceVersion);
            SetDevicePlatform(userLogRequest.DevicePlatform);
            SetDeviceLatitude(userLogRequest.DeviceLatitude);
            SetDeviceLongitude(userLogRequest.DeviceLongitude);
            SetIP(userLogRequest.IP);
            SetDeviceId(userLogRequest.DeviceId);
            ValidateModel();
        }

        public UserLogModel(string applicationVersion, string deviceName, string deviceVersion, string devicePlatform, double? deviceLatitude, double? deviceLongitude, string ip, DeviceTypeEnum deviceId)
        {
            SetApplicationVersion(applicationVersion);
            SetDeviceName(deviceName);
            SetDeviceVersion(deviceVersion);
            SetDevicePlatform(devicePlatform);
            SetDeviceLatitude(deviceLatitude);
            SetDeviceLongitude(deviceLongitude);
            SetIP(ip);
            SetDeviceId(deviceId);
            ValidateModel();
        }

        [JsonConstructor]
        public UserLogModel(int id, int? userId, UserLogActionEnum userLogActionId, DeviceTypeEnum deviceId, DateTime actionDate, string applicationVersion, string deviceName, string deviceVersion, string devicePlatform, double? deviceLatitude, double? deviceLongitude, string ip, UserLogActionModel userLogAction, DeviceTypeModel deviceType)
        {
            SetId(id);
            UserId = userId;
            UserLogActionId = userLogActionId;
            ActionDate = actionDate;
            UserLogAction = userLogAction;
            DeviceType = deviceType;
            SetApplicationVersion(applicationVersion);
            SetDeviceName(deviceName);
            SetDeviceVersion(deviceVersion);
            SetDevicePlatform(devicePlatform);
            SetDeviceLatitude(deviceLatitude);
            SetDeviceLongitude(deviceLongitude);
            SetIP(ip);
            SetDeviceId(deviceId);
        }
        #endregion

        #region Validate Model
        public override void ValidateModel()
        {
            ValidationResult = new UserLogValidation().Validate(this);
        } 
        #endregion

        #region Methods
        public void SetApplicationVersion(string applicationVersion)
        {
            ApplicationVersion = applicationVersion;
            ValidateModel();
        }

        public void SetDeviceName(string deviceName)
        {
            DeviceName = deviceName;
            ValidateModel();
        }

        public void SetDeviceVersion(string deviceVersion)
        {
            this.DeviceVersion = deviceVersion;
            ValidateModel();
        }

        public void SetDevicePlatform(string platform)
        {
            this.DevicePlatform = platform;
            ValidateModel();
        }

        public void SetDeviceLatitude(double? deviceLatitude)
        {
            this.DeviceLatitude = deviceLatitude;
        }

        public void SetDeviceLongitude(double? deviceLongitude)
        {
            this.DeviceLongitude = deviceLongitude;
        }

        public void SetIP(string ip)
        {
            this.IP = ip;
        }

        public void SetForgotPasswordAction()
        {
            this.UserLogActionId = UserLogActionEnum.ForgotPassword;
            this.ActionDate = DateTime.Now;
        }

        public void SetLoginAction()
        {
            this.UserLogActionId = UserLogActionEnum.Login;
            this.ActionDate = DateTime.Now;
        }

        public void SetActiveAccountAction()
        {
            this.UserLogActionId = UserLogActionEnum.ActivateAccount;
            this.ActionDate = DateTime.Now;
        }

        public void SetChangedPasswordAction()
        {
            this.UserLogActionId = UserLogActionEnum.ChangedPassword;
            this.ActionDate = DateTime.Now;
        }

        public void SetSignUpAction()
        {
            this.UserLogActionId = UserLogActionEnum.SignUp;
            this.ActionDate = DateTime.Now;
        }

        public void SetVerifyTokenAction()
        {
            this.UserLogActionId = UserLogActionEnum.VerifyToken;
            this.ActionDate = DateTime.Now;
        }

        public void SetCreatedUserAction()
        {
            this.UserLogActionId = UserLogActionEnum.CreatedUser;
            this.ActionDate = DateTime.Now;
        }

        public void SetUserRegistered()
        {
            this.UserLogActionId = UserLogActionEnum.UserRegistered;
            this.ActionDate = DateTime.Now;
        }

        public void SetUserId(int? userId) => this.UserId = userId;

        public void SetDeviceId(DeviceTypeEnum deviceId) => this.DeviceId = deviceId;

        public void SetDevice(DeviceTypeModel device) => this.DeviceType = device;
        public void SetUserLogAction(UserLogActionModel logAction) => this.UserLogAction = logAction;

        #endregion

        #region Fluent Validation
        public class UserLogValidation : AbstractValidator<UserLogModel>
        {
            public UserLogValidation()
            {
                RuleFor(x => x.ApplicationVersion).MaximumLength(10).WithMessage("{PropertyName} lenght cannot by greater 10");
                RuleFor(x => x.DeviceName).MaximumLength(30).WithMessage("{PropertyName} lenght cannot by greater 30");
                RuleFor(x => x.DeviceVersion).MaximumLength(10).WithMessage("{PropertyName} lenght cannot by greater 10");
                RuleFor(x => x.DevicePlatform).MaximumLength(10).WithMessage("{PropertyName} lenght cannot by greater 10");
            }
        }
        #endregion
    }
}
