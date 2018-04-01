using SimpleCarrier.Domain.Entities.Base;
using System.Collections.Generic;

namespace SimpleCarrier.Domain.Entities.Users
{
    public class User : DomainEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICollection<Role> Roles { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
