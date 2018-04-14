using SimpleCarrier.Domain.Entities.Base;
using System.Collections.Generic;

namespace SimpleCarrier.Domain.Entities.Users
{
    public class User : DomainEntity
    {
        public string UserName { get; set; }

        public ICollection<Role> Roles { get; set; } = new HashSet<Role>();
        public UserProfile UserProfile { get; set; }
    }
}
