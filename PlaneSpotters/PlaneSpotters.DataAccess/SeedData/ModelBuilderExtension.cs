using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotters.DataAccess.SeedData
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed Data for Admin Roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Admin", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName = "Admin" },
                new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Spotter", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName = "Spotter" }
                );
        }
    }
}
