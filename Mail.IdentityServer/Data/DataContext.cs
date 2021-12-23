using Mail.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mail.IdentityServer.Data
{

    public class DataContext: IdentityDbContext<User, Role, long>
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {

        }
    }
}
