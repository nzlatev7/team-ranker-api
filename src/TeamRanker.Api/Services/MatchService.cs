using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamRanker.Api.Data;
using TeamRanker.Core.Entities;
using TeamRanker.Core.Interfaces;

namespace TeamRanker.Api.Services
{

    public class MatchService : IMatchService
    {
        private readonly TeamRankerDbContext _context;

        public MatchService(TeamRankerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Match>> GetAllAsync()
        {
            return await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .AsNoTracking()
                .OrderByDescending(m => m.PlayedOn)
                .ToListAsync();
        }

        public async Task<Match?> GetByIdAsync(int id)
        {
            return await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Match> CreateAsync(Match match)
        {
            await ValidateTeamsExist(match);

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();
            await _context.Entry(match).Reference(m => m.HomeTeam).LoadAsync();
            await _context.Entry(match).Reference(m => m.AwayTeam).LoadAsync();
            return match;
        }

        public async Task<Match?> UpdateAsync(int id, Match match)
        {
            var existing = await _context.Matches.FirstOrDefaultAsync(m => m.Id == id);
            if (existing is null)
            {
                return null;
            }

            await ValidateTeamsExist(match);

            existing.HomeTeamId = match.HomeTeamId;
            existing.AwayTeamId = match.AwayTeamId;
            existing.HomeScore = match.HomeScore;
            existing.AwayScore = match.AwayScore;
            existing.PlayedOn = match.PlayedOn;

            await _context.SaveChangesAsync();
            await _context.Entry(existing).Reference(m => m.HomeTeam).LoadAsync();
            await _context.Entry(existing).Reference(m => m.AwayTeam).LoadAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Matches.FirstOrDefaultAsync(m => m.Id == id);
            if (existing is null)
            {
                return false;
            }

            _context.Matches.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task ValidateTeamsExist(Match match)
        {
            if (match.HomeTeamId == match.AwayTeamId)
            {
                throw new InvalidOperationException("A team cannot play against itself.");
            }

            bool homeExists = await _context.Teams.AnyAsync(t => t.Id == match.HomeTeamId);
            bool awayExists = await _context.Teams.AnyAsync(t => t.Id == match.AwayTeamId);

            if (!homeExists || !awayExists)
            {
                throw new InvalidOperationException("Both teams must exist before creating a match.");
            }
        }
    }
}
