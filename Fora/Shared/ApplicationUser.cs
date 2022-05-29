using Microsoft.AspNetCore.Identity;

namespace Fora.Shared;

public class ApplicationUser : IdentityUser
{        
    public string? Token { get; set; }
    
}
