using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamRanker.Api.Data;
using TeamRanker.Core.Interfaces;
using TeamRanker.Core.Models;

namespace TeamRanker.Api.Services;

public class RankingService : IRankingService
{
    private readonly TeamRankerDbContext _context;
    private readonly IRankingStrategy _rankingStrategy;

    public RankingService(TeamRankerDbContext context, IRankingStrategy rankingStrategy)
    {
        _context = context;
        _rankingStrategy = rankingStrategy;
    }

    public async Task<IReadOnlyCollection<TeamStanding>> GetStandingsAsync()
    {
        var teams = await _context.Teams.AsNoTracking().ToListAsync();
        var matches = await _context.Matches.AsNoTracking().ToListAsync();
        return _rankingStrategy.CalculateStandings(teams, matches);
    }
}
