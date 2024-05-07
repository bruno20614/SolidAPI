using Microsft.EntityFramerworkCore;
using Manager.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Context;

public class ManagerContext : DBcontext
{
    public ManagerContext()
    {
        
    }
	
	public ManagerContext(DbContextOptions<ManagerContext> options) :base(options)
	{

	}

	public override void OnConfiguration(DBContextOptionsBuilder optionsbuilder)
	{
		optionsbuilder.UserSqlServer("Server=PCBRUNO\SQLEXPRESS;Database=USERMANAGERAPI;Integrated Security=SSPI;TrustServerCertificate=True;trusted_connection=true")
	}

	public virtual DbSet<User> Users{Get,Set;}
	
	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfiguration(new UserMap());
	}
}