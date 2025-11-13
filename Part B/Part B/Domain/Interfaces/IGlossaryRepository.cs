using Part_B.Domain.Entities;

namespace Part_B.Domain.Interfaces;

public interface IGlossaryRepository
{
    Task<IReadOnlyList<GlossaryTerm>> GetAllAsync(CancellationToken cancellationToken);
    Task<GlossaryTerm?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(GlossaryTerm glossaryTerm, CancellationToken cancellationToken);
    Task RemoveAsync(GlossaryTerm glossaryTerm, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}