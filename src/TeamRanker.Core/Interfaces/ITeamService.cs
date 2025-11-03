using System.Collections.Generic;
using System.Threading.Tasks;
using TeamRanker.Core.Entities;

namespace TeamRanker.Core.Interfaces
{

    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetAllAsync();
        Task<Team?> GetByIdAsync(int id);
        Task<Team> CreateAsync(Team team);
        Task<Team?> UpdateAsync(int id, Team team);
        Task<bool> DeleteAsync(int id);
    }
}
