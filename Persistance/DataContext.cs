using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance
{
    public class DataContext:IdentityDbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { 
        }
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
