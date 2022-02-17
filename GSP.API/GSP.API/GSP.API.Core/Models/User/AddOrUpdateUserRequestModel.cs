using GSP.API.Core.Models.Application;
using System.Collections.Generic;

namespace GSP.API.Core.Models.User
{
    public class AddOrUpdateUserRequestModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string ClientIP { get; set; }
        public UserLogRequestModel UserLog { get; set; }
        public int UserRoleId { get; set; }
        public UserRoleModel UserRole { get; set; } = new UserRoleModel();
        public List<UserRoleModel> UserRoles { get; set; } = new List<UserRoleModel>();
    }
}
