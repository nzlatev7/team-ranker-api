using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamRanker.Api.Data;
using TeamRanker.Core.Entities;
using TeamRanker.Core.Interfaces;

namespace TeamRanker.Api.Services
{

    public class TeamService : ITeamService
    {
        private readonly TeamRankerDbContext _context;

        public TeamService(TeamRankerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _context.Teams
                .AsNoTracking()
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<Team?> GetByIdAsync(int id)
        {
            return await _context.Teams
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Team> CreateAsync(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<Team?> UpdateAsync(int id, Team team)
        {
            var existing = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
            if (existing is null)
            {
                return null;
            }

            existing.Name = team.Name;
            existing.City = team.City;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
            if (entity is null)
            {
                return false;
            }

            _context.Teams.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
