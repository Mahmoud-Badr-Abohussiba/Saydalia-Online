using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Saydalia_Online.Areas.Identity.Data;
using System;

namespace Saydalia_Online.Models
{
    public class SaydaliaOnlineContext : IdentityDbContext<Saydalia_Online_AuthUser>
    {
        public SaydaliaOnlineContext(DbContextOptions<SaydaliaOnlineContext> options) : base(options)
        {
        }

        public DbSet<Category> categories { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            base.OnModelCreating(builder);
            // Customize your model if needed
        }
    }
}
