using Microsoft.AspNetCore.Identity;

namespace AuthenticationWithIdentity.Models
{
    public class AppUser : IdentityUser
    { 
        public string? FullName { get; set; }
        public int Contact { get; set; } 
        public string? Address { get; set; }
    }
}
