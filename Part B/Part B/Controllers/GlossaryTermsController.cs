using Microsoft.AspNetCore.Mvc;
using Part_B.Domain.Dtos;
using Part_B.Domain.Interfaces;

namespace Part_B.Controllers;

[ApiController]
[Route("api/glossary-terms")]
public class GlossaryTermsController : ControllerBase
{
    private readonly IGlossaryService _glossaryService;

    public GlossaryTermsController(IGlossaryService glossaryService)
    {
        _glossaryService = glossaryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GlossaryTermResponseDto>>> GetAll(CancellationToken ct)
    {
        var result = await _glossaryService.GetAllAsync(ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GlossaryTermResponseDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _glossaryService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GlossaryTermResponseDto>> Create([FromBody] GlossaryTermRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _glossaryService.CreateAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<GlossaryTermResponseDto>> Update(Guid id, [FromBody] GlossaryTermRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _glossaryService.UpdateAsync(id, request, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _glossaryService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}