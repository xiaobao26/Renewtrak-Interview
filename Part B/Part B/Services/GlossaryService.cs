using Part_B.Domain.Dtos;
using Part_B.Domain.Entities;
using Part_B.Domain.Interfaces;

namespace Part_B.Services;

public class GlossaryService : IGlossaryService
{
    private readonly IGlossaryRepository _glossaryRepository;
    
    public GlossaryService(IGlossaryRepository glossaryRepository)
    {
        _glossaryRepository = glossaryRepository;
    }
    
    public async Task<IReadOnlyList<GlossaryTermDto>> GetAllAsync(CancellationToken ct)
    {
        var entities = await _glossaryRepository.GetAllAsync(ct);
        return entities.Select(e => new GlossaryTermDto(e.Id, e.Term, e.Definition)).ToList();
    }

    public async Task<GlossaryTermDto?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var entity = await _glossaryRepository.GetByIdAsync(id, ct);
        
        if (entity is null) 
            throw new Exception($"Cannot found {id}");

        return new GlossaryTermDto(entity.Id, entity.Term, entity.Definition);
    }

    public async Task<GlossaryTermDto> CreateAsync(GlossaryTermDto dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.Term) || string.IsNullOrWhiteSpace(dto.Definition))
            throw new ArgumentException("Term and Definition are required.");

        var entity = new GlossaryTerm
        {
            Term = dto.Term.Trim(),
            Definition = dto.Definition.Trim()
        };

        await _glossaryRepository.AddAsync(entity, ct);
        await _glossaryRepository.SaveChangesAsync(ct);

        return new GlossaryTermDto(entity.Id, entity.Term, entity.Definition);
    }

    public async Task<GlossaryTermDto?> UpdateAsync(Guid id, GlossaryTermDto dto, CancellationToken ct)
    {
        var entity = await _glossaryRepository.GetByIdAsync(id, ct);
        if (entity is null) 
           throw new Exception($"Cannot found {id}");

        entity.Term       = dto.Term.Trim();
        entity.Definition = dto.Definition.Trim();
        await _glossaryRepository.SaveChangesAsync(ct);

        return new GlossaryTermDto(entity.Id, entity.Term, entity.Definition);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _glossaryRepository.GetByIdAsync(id, ct);
        if (entity is null)
            return false;

        await _glossaryRepository.RemoveAsync(entity, ct);
        await _glossaryRepository.SaveChangesAsync(ct);
        return true;
    }
}