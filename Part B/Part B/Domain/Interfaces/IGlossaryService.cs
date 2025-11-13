using Part_B.Domain.Dtos;

namespace Part_B.Domain.Interfaces;

public interface IGlossaryService
{
    Task<IReadOnlyList<GlossaryTermResponseDto>> GetAllAsync(CancellationToken ct);
    Task<GlossaryTermResponseDto> GetByIdAsync(Guid id, CancellationToken ct);
    Task<GlossaryTermResponseDto> CreateAsync(GlossaryTermRequestDto request, CancellationToken ct);
    Task<GlossaryTermResponseDto> UpdateAsync(Guid id, GlossaryTermRequestDto request, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}