using Microsoft.EntityFrameworkCore;
using Part_B.Domain.Entities;
using Part_B.Domain.Interfaces;

namespace Part_B.Infrastructure.Repositories;

public class GlossaryRepository : IGlossaryRepository
{
    private readonly AppDbContext _context;

    public GlossaryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IReadOnlyList<GlossaryTerm>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.GlossaryTerms
            .AsNoTracking()
            .OrderBy(gt => gt.Term)
            .ToListAsync(cancellationToken);
    }

    public async Task<GlossaryTerm?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.GlossaryTerms
            .FindAsync([id], cancellationToken);
    }

    public async Task AddAsync(GlossaryTerm glossaryTerm, CancellationToken cancellationToken)
    {
        await _context.GlossaryTerms.AddAsync(glossaryTerm, cancellationToken);
    }

    public Task RemoveAsync(GlossaryTerm glossaryTerm, CancellationToken cancellationToken)
    {
        _context.GlossaryTerms.Remove(glossaryTerm);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}