using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Fora.Shared;
using Microsoft.EntityFrameworkCore;

namespace Fora.Server.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
    }
}
