namespace TeamRanker.Core.Models;

public record TeamStanding(
    int TeamId,
    string TeamName,
    int Played,
    int Wins,
    int Draws,
    int Losses,
    int GoalsFor,
    int GoalsAgainst,
    int Points);
