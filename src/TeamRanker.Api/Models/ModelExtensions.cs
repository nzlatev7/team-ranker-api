using TeamRanker.Core.Entities;
using TeamRanker.Core.Models;

namespace TeamRanker.Api.Models
{

    public static class ModelExtensions
    {
        public static TeamDto ToDto(this Team team) => new()
        {
            Id = team.Id,
            Name = team.Name,
            City = team.City
        };

        public static void UpdateEntity(this Team team, TeamRequest request)
        {
            team.Name = request.Name;
            team.City = request.City;
        }

        public static MatchDto ToDto(this Match match) => new()
        {
            Id = match.Id,
            HomeTeamId = match.HomeTeamId,
            HomeTeamName = match.HomeTeam?.Name,
            AwayTeamId = match.AwayTeamId,
            AwayTeamName = match.AwayTeam?.Name,
            HomeScore = match.HomeScore,
            AwayScore = match.AwayScore,
            PlayedOn = match.PlayedOn
        };

        public static void UpdateEntity(this Match match, MatchRequest request)
        {
            match.HomeTeamId = request.HomeTeamId;
            match.AwayTeamId = request.AwayTeamId;
            match.HomeScore = request.HomeScore;
            match.AwayScore = request.AwayScore;
            match.PlayedOn = request.PlayedOn;
        }

        public static RankingDto ToDto(this TeamStanding standing) => new()
        {
            TeamId = standing.TeamId,
            TeamName = standing.TeamName,
            Played = standing.Played,
            Wins = standing.Wins,
            Draws = standing.Draws,
            Losses = standing.Losses,
            GoalsFor = standing.GoalsFor,
            GoalsAgainst = standing.GoalsAgainst,
            Points = standing.Points
        };
    }
}
