using Microsoft.AspNetCore.Identity;

namespace Fora.Server.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string Token { get; set; }

    }
}
