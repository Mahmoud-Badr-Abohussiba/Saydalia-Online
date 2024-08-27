using Microsoft.EntityFrameworkCore;

namespace Saydalia_Online.Models
{
	public class SaydaliaOnlineContext : DbContext
	{
		public DbSet<Category> categories { get; set; }
		public DbSet<Medicine> Medicines { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server= DESKTOP-00JH14J\\MSSQLSERVER08; Database= DepiMVCTask2; Trusted_Connection= True; TrustServerCertificate= True;");
			base.OnConfiguring(optionsBuilder);
		}


	}
}
