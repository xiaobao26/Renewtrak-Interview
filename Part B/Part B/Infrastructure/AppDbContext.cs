using Microsoft.EntityFrameworkCore;
using Part_B.Domain;

namespace Part_B.Infrastructure;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<GlossaryTerm> GlossaryTerms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GlossaryTerm>(e =>
        {
            e.ToTable("GlossaryTerms");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            e.Property(x => x.Term).IsRequired().HasMaxLength(100);
            e.Property(x => x.Definition).IsRequired().HasMaxLength(4000);

            e.HasIndex(x => x.Term).IsUnique();
        });
    }
}