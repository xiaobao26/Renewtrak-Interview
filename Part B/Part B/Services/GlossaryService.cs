using Part_B.Domain.Dtos;
using Part_B.Domain.Entities;
using Part_B.Domain.Exceptions;
using Part_B.Domain.Interfaces;

namespace Part_B.Services;

public class GlossaryService : IGlossaryService
{
    private readonly IGlossaryRepository _glossaryRepository;
    
    public GlossaryService(IGlossaryRepository glossaryRepository)
    {
        _glossaryRepository = glossaryRepository;
    }
    
    public async Task<IReadOnlyList<GlossaryTermResponseDto>> GetAllAsync(CancellationToken ct)
    {
        var entities = await _glossaryRepository.GetAllAsync(ct);
        return entities.Select(e => new GlossaryTermResponseDto(e.Id, e.Term, e.Definition)).ToList();
    }

    public async Task<GlossaryTermResponseDto?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var entity = await _glossaryRepository.GetByIdAsync(id, ct);
        
        if (entity is null) 
            throw new NotFoundException(nameof(GlossaryTerm), id.ToString());

        return new GlossaryTermResponseDto(entity.Id, entity.Term, entity.Definition);
    }

    public async Task<GlossaryTermResponseDto> CreateAsync(GlossaryTermRequestDto request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Term) || string.IsNullOrWhiteSpace(request.Definition))
            throw new ArgumentException("Term and Definition are required.");

        var entity = new GlossaryTerm
        {
            Term = request.Term.Trim(),
            Definition = request.Definition.Trim()
        };

        await _glossaryRepository.AddAsync(entity, ct);
        await _glossaryRepository.SaveChangesAsync(ct);

        return new GlossaryTermResponseDto(entity.Id, entity.Term, entity.Definition);
    }

    public async Task<GlossaryTermResponseDto?> UpdateAsync(Guid id, GlossaryTermRequestDto request, CancellationToken ct)
    {
        var entity = await _glossaryRepository.GetByIdAsync(id, ct);
        if (entity is null) 
            throw new NotFoundException(nameof(GlossaryTerm), id.ToString());

        entity.Term       = request.Term.Trim();
        entity.Definition = request.Definition.Trim();
        await _glossaryRepository.SaveChangesAsync(ct);

        return new GlossaryTermResponseDto(entity.Id, entity.Term, entity.Definition);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _glossaryRepository.GetByIdAsync(id, ct);
        if (entity is null)
            throw new NotFoundException(nameof(GlossaryTerm), id.ToString());

        await _glossaryRepository.RemoveAsync(entity, ct);
        await _glossaryRepository.SaveChangesAsync(ct);
    }
}