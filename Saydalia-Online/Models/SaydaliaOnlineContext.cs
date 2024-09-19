
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Saydalia_Online.Models
{
	public class SaydaliaOnlineContext : IdentityDbContext<IdentityUser>
	{
		public SaydaliaOnlineContext(DbContextOptions<SaydaliaOnlineContext> options) : base(options)
		{
		}
		public DbSet<Category> categories { get; set; }
		public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
				.HasIndex(c => c.Name)
				.IsUnique();

			base.OnModelCreating(modelBuilder);
		}



		//      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlServer("Server=.; Database= SaydaliaOnline; Trusted_Connection= True; TrustServerCertificate= True;");
		//	base.OnConfiguring(optionsBuilder);
		//}


	}
}
