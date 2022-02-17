using Newtonsoft.Json;

namespace GSP.API.Core.Models.User
{
    public class UserLogActionModel
    {
        #region Properties
        public int Id { get; private set; }
        public string Description { get; private set; }
        #endregion

        #region Constructor
        public UserLogActionModel() { }

        [JsonConstructor]
        public UserLogActionModel(int id, string description)
        {
            Id = id;
            Description = description;
        }
        #endregion
    }
}
