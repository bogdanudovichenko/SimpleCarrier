using SimpleCarrier.Domain.Entities.Base;
using System.Collections.Generic;

namespace SimpleCarrier.Domain.Entities.Users
{
    public class Role : DomainEntity
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } 
    }
}