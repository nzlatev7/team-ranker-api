using System.Collections.Generic;
using System.Linq;
using TeamRanker.Core.Entities;
using TeamRanker.Core.Interfaces;
using TeamRanker.Core.Models;

namespace TeamRanker.Core.Services;

/// <summary>
/// Implements the standard 3-1-0 point system used in many leagues.
/// This demonstrates the Strategy pattern – the algorithm used to
/// calculate the standings is encapsulated and can be swapped with a
/// different implementation without changing the rest of the system.
/// </summary>
public class StandardRankingStrategy : IRankingStrategy
{
    private const int WinPoints = 3;
    private const int DrawPoints = 1;

    public IReadOnlyCollection<TeamStanding> CalculateStandings(IEnumerable<Team> teams, IEnumerable<Match> matches)
    {
        return teams
            .Select(team => BuildStanding(team, matches))
            .OrderByDescending(s => s.Points)
            .ThenByDescending(s => s.GoalsFor - s.GoalsAgainst)
            .ThenByDescending(s => s.GoalsFor)
            .ThenBy(s => s.TeamName)
            .ToList();
    }

    private static TeamStanding BuildStanding(Team team, IEnumerable<Match> matches)
    {
        var playedMatches = matches.Where(m => m.HomeTeamId == team.Id || m.AwayTeamId == team.Id);

        int wins = 0;
        int draws = 0;
        int losses = 0;
        int goalsFor = 0;
        int goalsAgainst = 0;

        foreach (var match in playedMatches)
        {
            bool isHome = match.HomeTeamId == team.Id;
            int teamScore = isHome ? match.HomeScore : match.AwayScore;
            int opponentScore = isHome ? match.AwayScore : match.HomeScore;

            goalsFor += teamScore;
            goalsAgainst += opponentScore;

            if (teamScore > opponentScore)
            {
                wins++;
            }
            else if (teamScore == opponentScore)
            {
                draws++;
            }
            else
            {
                losses++;
            }
        }

        int played = wins + draws + losses;
        int points = wins * WinPoints + draws * DrawPoints;

        return new TeamStanding(
            team.Id,
            team.Name,
            played,
            wins,
            draws,
            losses,
            goalsFor,
            goalsAgainst,
            points);
    }
}
