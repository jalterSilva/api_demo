using System;

namespace GSP.API.Core.Models.User
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; private set; }
        public DateTime? TokenDate { get; set; }
        public bool Active { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? InactiveDate { get; set; }
    }
}
