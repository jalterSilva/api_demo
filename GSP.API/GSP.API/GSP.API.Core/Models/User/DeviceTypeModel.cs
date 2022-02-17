using Newtonsoft.Json;

namespace GSP.API.Core.Models.User
{
    public class DeviceTypeModel
    {
        #region Properties
        public int Id { get; set; }
        public string DeviceTypeName { get; private set; }
        public bool Active { get; private set; }
        #endregion

        #region Constructor
        public DeviceTypeModel()
        {

        }

        [JsonConstructor]
        public DeviceTypeModel(int id, string deviceTypeName, bool active)
        {
            SetId(id);
            SetDeviceName(deviceTypeName);

            if (active)
                SetActive();
            else
                SetInactive();
        }
        #endregion

        #region Methods
        private void SetId(int id) => this.Id = id;
        private void SetDeviceName(string deviceName) => this.DeviceTypeName = deviceName;
        private void SetActive() => this.Active = true;
        private void SetInactive() => this.Active = false;
        #endregion

    }
}
