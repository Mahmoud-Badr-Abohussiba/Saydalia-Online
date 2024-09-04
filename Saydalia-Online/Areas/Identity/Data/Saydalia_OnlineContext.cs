using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Saydalia_Online.Areas.Identity.Data;

namespace Saydalia_Online.Data;

public class Saydalia_OnlineContext : IdentityDbContext<Saydalia_Online_AuthUser>
{
    public Saydalia_OnlineContext(DbContextOptions<Saydalia_OnlineContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.; Database= SaydaliaOnline; Trusted_Connection= True; TrustServerCertificate= True;");
        base.OnConfiguring(optionsBuilder);
    }
}
