using System.Collections.Generic;
using System.Threading.Tasks;
using TeamRanker.Core.Models;

namespace TeamRanker.Core.Interfaces;

public interface IRankingService
{
    Task<IReadOnlyCollection<TeamStanding>> GetStandingsAsync();
}
