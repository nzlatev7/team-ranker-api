using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamRanker.Api.Models;
using TeamRanker.Core.Entities;
using TeamRanker.Core.Interfaces;

namespace TeamRanker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchesController : ControllerBase
{
    private readonly IMatchService _matchService;

    public MatchesController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MatchDto>>> GetAllAsync()
    {
        var matches = await _matchService.GetAllAsync();
        return Ok(matches.Select(m => m.ToDto()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MatchDto>> GetByIdAsync(int id)
    {
        var match = await _matchService.GetByIdAsync(id);
        if (match is null)
        {
            return NotFound();
        }

        return Ok(match.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<MatchDto>> CreateAsync([FromBody] MatchRequest request)
    {
        var match = new Match();
        match.UpdateEntity(request);
        var created = await _matchService.CreateAsync(match);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created.ToDto());
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<MatchDto>> UpdateAsync(int id, [FromBody] MatchRequest request)
    {
        var match = new Match();
        match.UpdateEntity(request);
        var updated = await _matchService.UpdateAsync(id, match);
        if (updated is null)
        {
            return NotFound();
        }

        return Ok(updated.ToDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var removed = await _matchService.DeleteAsync(id);
        if (!removed)
        {
            return NotFound();
        }

        return NoContent();
    }
}
