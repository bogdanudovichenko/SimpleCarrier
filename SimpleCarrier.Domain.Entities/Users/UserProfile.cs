using SimpleCarrier.Domain.Entities.Base;

namespace SimpleCarrier.Domain.Entities.Users
{
    public class UserProfile : DomainEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
