using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamRanker.Api.Models;
using TeamRanker.Core.Entities;
using TeamRanker.Core.Interfaces;

namespace TeamRanker.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetAllAsync()
        {
            var teams = await _teamService.GetAllAsync();
            return Ok(teams.Select(t => t.ToDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TeamDto>> GetByIdAsync(int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team is null)
            {
                return NotFound();
            }

            return Ok(team.ToDto());
        }

        [HttpPost]
        public async Task<ActionResult<TeamDto>> CreateAsync([FromBody] TeamRequest request)
        {
            var team = new Team();
            team.UpdateEntity(request);
            var created = await _teamService.CreateAsync(team);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created.ToDto());
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TeamDto>> UpdateAsync(int id, [FromBody] TeamRequest request)
        {
            var team = new Team();
            team.UpdateEntity(request);
            var updated = await _teamService.UpdateAsync(id, team);
            if (updated is null)
            {
                return NotFound();
            }

            return Ok(updated.ToDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var removed = await _teamService.DeleteAsync(id);
            if (!removed)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
