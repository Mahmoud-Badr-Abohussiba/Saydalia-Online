using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Saydalia_Online.Models
{
	public class SaydaliaOnlineContext : IdentityDbContext<IdentityUser>
	{
		public DbSet<Category> categories { get; set; }
		public DbSet<Medicine> Medicines { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SaydaliaOnline;Integrated Security=True;Trust Server Certificate=True ");
			base.OnConfiguring(optionsBuilder);
		}


	}
}
