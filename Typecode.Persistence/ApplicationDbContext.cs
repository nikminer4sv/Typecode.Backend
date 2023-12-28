using Typecode.Domain;
using Microsoft.EntityFrameworkCore;
using Typecode.Persistence.EntityTypeConfigurations;

namespace Typecode.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<FinishedTest> FinishedTests { get; set; }
    public DbSet<Test> Tests { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new TestConfiguration());
        builder.ApplyConfiguration(new TestConfiguration());
        base.OnModelCreating(builder);
    }
}