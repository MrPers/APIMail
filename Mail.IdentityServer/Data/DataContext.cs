using Mail.IdentityServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mail.IdentityServer.Data
{

    public class DataContext: IdentityDbContext<User, Role, long>
    {
        //public DbSet<Claim> Claims { get; set; }
        //public new DbSet<UserClaim> UserClaims { get; set; }
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {

        }

    }
}
