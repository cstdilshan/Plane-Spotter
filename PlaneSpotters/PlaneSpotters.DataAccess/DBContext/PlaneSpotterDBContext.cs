using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlaneSpotters.Core.Entities;
using PlaneSpotters.Core.Handlers;
using PlaneSpotters.DataAccess.SeedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlaneSpotters.DataAccess.Data
{
    public class PlaneSpotterDBContext : IdentityDbContext<ApplicationUser>
    {
        public PlaneSpotterDBContext(DbContextOptions<PlaneSpotterDBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            //modelBuilder.BuildIndexesFromAnnotations();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                var now = DateTime.UtcNow;
                dynamic entity = entry.Entity;

                if (ObjectHandler.IsPropertyExist(entity, "UpdatedOn"))
                {
                    if (entry.State == EntityState.Added)
                    {
                        if (ObjectHandler.IsPropertyExist(entity, "CreatedOn"))
                        {
                            entity.CreatedOn = now;
                        }
                        if (ObjectHandler.IsPropertyExist(entity, "InternalId"))
                        {
                            entity.InternalId = Guid.NewGuid();
                        }
                        entity.UpdatedOn = now;
                    }
                    else
                    {
                        entity.UpdatedOn = now;
                    }
                }
            }
            this.ChangeTracker.DetectChanges();
            return base.SaveChangesAsync();
        }

        public DbSet<PlaneSpotter> PlaneSpotters { get; set; }
    }
}
