namespace SimpleCarrier.Infrastructure.Repositories.DbModels
{
    public class UserRoleDbModel : BaseDbModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
