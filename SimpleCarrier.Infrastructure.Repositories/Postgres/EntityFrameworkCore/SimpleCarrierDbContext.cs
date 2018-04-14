using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleCarrier.Infrastructure.Repositories.DbModels;

namespace SimpleCarrier.Infrastructure.Repositories.Postgres.EntityFrameworkCore
{
    public class SimpleCarrierDbContext : IdentityDbContext<UserDbModel, RoleDbModel, int>
    {
        public SimpleCarrierDbContext(DbContextOptions<SimpleCarrierDbContext> options) : base(options)
        {
        }
    }
}
