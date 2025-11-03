using System.Collections.Generic;
using System.Threading.Tasks;
using TeamRanker.Core.Entities;

namespace TeamRanker.Core.Interfaces;

public interface IMatchService
{
    Task<IEnumerable<Match>> GetAllAsync();
    Task<Match?> GetByIdAsync(int id);
    Task<Match> CreateAsync(Match match);
    Task<Match?> UpdateAsync(int id, Match match);
    Task<bool> DeleteAsync(int id);
}
