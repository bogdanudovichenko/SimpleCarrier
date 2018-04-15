using Microsoft.AspNetCore.Identity;

namespace SimpleCarrier.Infrastructure.Repositories.DbModels
{
    public class UserDbModel : IdentityUser<int>
    {
        public UserProfileDbModel UserProfile { get; set; }
    }
}
