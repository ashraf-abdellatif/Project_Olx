using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    public class DataContext:IdentityDbContext<AppUser , Role , string , IdentityUserClaim<string> , UserRoles , IdentityUserLogin<string> , IdentityRoleClaim<string> , IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions options) : base(options)
        { 
            
        }
        public DbSet<UserRoles> UserRole { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder Builder)
        {
            base.OnModelCreating(Builder);
            Builder.Entity<UserRoles>(opt =>
            {
                opt.HasKey(x => new { x.RoleId, x.UserId });

                opt.HasOne(x => x.Role)
                   .WithMany(x => x.UserRoles)
                   .HasForeignKey(x => x.RoleId)
                   .IsRequired();

                opt.HasOne(x => x.AppUser)
                  .WithMany(x => x.UserRoles)
                  .HasForeignKey(x => x.UserId)
                  .IsRequired();
            }
            );
        }
    }
}
