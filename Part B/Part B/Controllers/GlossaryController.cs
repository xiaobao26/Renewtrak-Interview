using Microsoft.AspNetCore.Mvc;
using Part_B.Domain.Dtos;
using Part_B.Domain.Interfaces;

namespace Part_B.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GlossaryController : ControllerBase
{
    private readonly IGlossaryService _glossaryService;

    public GlossaryController(IGlossaryService glossaryService)
    {
        _glossaryService = glossaryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GlossaryTermDto>>> GetAll(CancellationToken ct)
    {
        var result = await _glossaryService.GetAllAsync(ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GlossaryTermDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _glossaryService.GetByIdAsync(id, cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GlossaryTermDto>> Create([FromBody] GlossaryTermDto request,
        CancellationToken cancellationToken)
    {
        var result = await _glossaryService.CreateAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<GlossaryTermDto>> Update(Guid id, [FromBody] GlossaryTermDto request,
        CancellationToken cancellationToken)
    {
        var result = await _glossaryService.UpdateAsync(id, request, cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _glossaryService.DeleteAsync(id, cancellationToken);
        if (!result)
            return NotFound();

        return NoContent();
    }
}