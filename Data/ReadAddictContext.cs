using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReadAddict.Data.Entities;

namespace ReadAddict.Data
{
    public class ReadAddictContext : IdentityDbContext<AppUser>
    {
        public ReadAddictContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> appUsers { get; set; }
    }
}
