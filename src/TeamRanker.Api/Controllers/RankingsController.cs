using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamRanker.Api.Models;
using TeamRanker.Core.Interfaces;

namespace TeamRanker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RankingsController : ControllerBase
{
    private readonly IRankingService _rankingService;

    public RankingsController(IRankingService rankingService)
    {
        _rankingService = rankingService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RankingDto>>> GetAsync()
    {
        var standings = await _rankingService.GetStandingsAsync();
        return Ok(standings.Select(s => s.ToDto()));
    }
}
