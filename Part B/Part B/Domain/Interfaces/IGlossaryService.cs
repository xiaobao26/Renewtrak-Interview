using Part_B.Domain.Dtos;

namespace Part_B.Domain.Interfaces;

public interface IGlossaryService
{
    Task<IReadOnlyList<GlossaryTermDto>> GetAllAsync(CancellationToken ct);
    Task<GlossaryTermDto?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<GlossaryTermDto> CreateAsync(GlossaryTermDto dto, CancellationToken ct);
    Task<GlossaryTermDto?> UpdateAsync(Guid id, GlossaryTermDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct);
}