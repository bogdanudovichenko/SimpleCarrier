namespace SimpleCarrier.Infrastructure.Repositories.DbModels
{
    public class UserProfileDbModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int UserId { get; set; }
        public UserDbModel User { get; set; }
    }
}
