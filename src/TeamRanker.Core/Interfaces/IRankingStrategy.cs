using System.Collections.Generic;
using TeamRanker.Core.Entities;
using TeamRanker.Core.Models;

namespace TeamRanker.Core.Interfaces
{

    public interface IRankingStrategy
    {
        IReadOnlyCollection<TeamStanding> CalculateStandings(IEnumerable<Team> teams, IEnumerable<Match> matches);
    }
}
