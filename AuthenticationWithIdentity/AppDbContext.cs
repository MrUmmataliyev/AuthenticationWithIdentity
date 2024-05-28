using AuthenticationWithIdentity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationWithIdentity
{
    public class AppDbContext : IdentityDbContext<AppUser>   //was IdentityUser
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
            Database.Migrate(); //Databasani Update qilish uchun
        }
    }
}
