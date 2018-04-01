namespace SimpleCarrier.Infrastructure.Repositories.DbModels
{
    public class UserDbModel : BaseDbModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserProfileId { get; set; }
    }
}
