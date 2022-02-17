using System.Collections.Generic;

namespace GSP.API.Core.Models.User
{
    public class UserHerdRequestModel
    {
        #region Properties
        public int UserId { get; set; }
        public List<string> HerdCodes { get; set; } 
        #endregion

        #region Constructor
        public UserHerdRequestModel()
        {
            HerdCodes = new List<string>();
        } 
        #endregion
    }
}
