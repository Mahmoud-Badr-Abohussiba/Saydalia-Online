using Microsoft.EntityFrameworkCore;

namespace Saydalia_Online.Models
{
	public class SaydaliaOnlineContext : DbContext
	{
		public DbSet<Category> categories { get; set; }
		public DbSet<Medicine> Medicines { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=.; Database= SaydaliaOnline; Trusted_Connection= True; TrustServerCertificate= True;");
			base.OnConfiguring(optionsBuilder);
		}


	}
}
