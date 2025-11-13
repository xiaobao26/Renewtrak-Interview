using Microsoft.EntityFrameworkCore;
using Part_B.Domain.Entities;

namespace Part_B.Infrastructure;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<GlossaryTerm> GlossaryTerms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<GlossaryTerm>().HasData(
            new GlossaryTerm
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa9"),
                Term = "abyssal plain",
                Definition = "The ocean floor offshore from the continental margin, usually very flat with a slight slope."
            },
            new GlossaryTerm
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb6"),
                Term = "accrete",
                Definition = "v. To add terranes (small land masses or pieces of crust) to another, usually larger, land mass."
            },
            new GlossaryTerm
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afc7"),
                Term = "alkaline",
                Definition = "Term pertaining to a highly basic, as opposed to acidic, subtance. For example, hydroxide or carbonate of sodium or potassium."
            }
        );

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