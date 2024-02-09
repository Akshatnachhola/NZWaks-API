using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalksAPI.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options) 
        { 
        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "655f8897-8cb2-4890-a6d8-75f00f5f2125";
            var writerRoleId = "333cf597-ad9c-4910-8b18-65ebc07441a4";

            var roles = new List<IdentityRole>
            {


             new IdentityRole
             {
                 Id = readerRoleId,
                 ConcurrencyStamp = readerRoleId,
                 Name = "Reader",
                 NormalizedName = "Reader".ToUpper()


             },
             new IdentityRole
             {
                 Id = writerRoleId,
                 ConcurrencyStamp =  writerRoleId,
                 Name = "Writer",
                 NormalizedName = "Writer".ToUpper()
             }

            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
