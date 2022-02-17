using GSP.API.Core.Models.Enum;

namespace GSP.API.Core.Models.Application
{
    public class UserLogRequestModel
    {
        public int UserId { get; set; }
        public string ApplicationVersion { get; set; }
        public DeviceTypeEnum DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceVersion { get; set; }
        public string DevicePlatform { get; set; }
        public double? DeviceLatitude { get; set; }
        public double? DeviceLongitude { get; set; }
        public string IP { get; set; }
    }
}
