using System;
using System.Collections.Generic;
using System.Linq;
using TeamRanker.Core.Entities;
using TeamRanker.Core.Services;
using Xunit;

namespace TeamRanker.Tests
{

    public class StandardRankingStrategyTests
    {
        [Fact]
        public void CalculateStandings_ComputesPointsAndOrdering()
        {
            var teams = new List<Team>
            {
                new() { Id = 1, Name = "Alpha" },
                new() { Id = 2, Name = "Beta" },
                new() { Id = 3, Name = "Gamma" }
            };

            var matches = new List<Match>
            {
                new() { Id = 1, HomeTeamId = 1, AwayTeamId = 2, HomeScore = 2, AwayScore = 0, PlayedOn = DateTime.UtcNow },
                new() { Id = 2, HomeTeamId = 2, AwayTeamId = 3, HomeScore = 1, AwayScore = 1, PlayedOn = DateTime.UtcNow },
                new() { Id = 3, HomeTeamId = 3, AwayTeamId = 1, HomeScore = 3, AwayScore = 2, PlayedOn = DateTime.UtcNow }
            };

            var strategy = new StandardRankingStrategy();

            var standings = strategy.CalculateStandings(teams, matches).ToList();

            Assert.Collection(standings,
                first =>
                {
                    Assert.Equal(1, first.TeamId);
                    Assert.Equal(3, first.Points);
                    Assert.Equal(2, first.Played);
                    Assert.Equal(1, first.Wins);
                    Assert.Equal(0, first.Draws);
                    Assert.Equal(1, first.Losses);
                },
                second =>
                {
                    Assert.Equal(3, second.TeamId);
                    Assert.Equal(3, second.Points);
                },
                third =>
                {
                    Assert.Equal(2, third.TeamId);
                    Assert.Equal(1, third.Points);
                });
        }

        [Fact]
        public void CalculateStandings_UsesGoalDifferenceForTies()
        {
            var teams = new List<Team>
            {
                new() { Id = 1, Name = "Alpha" },
                new() { Id = 2, Name = "Beta" },
                new() { Id = 3, Name = "Gamma" }
            };

            var matches = new List<Match>
            {
                new() { Id = 1, HomeTeamId = 1, AwayTeamId = 3, HomeScore = 4, AwayScore = 1, PlayedOn = DateTime.UtcNow },
                new() { Id = 2, HomeTeamId = 2, AwayTeamId = 3, HomeScore = 1, AwayScore = 0, PlayedOn = DateTime.UtcNow }
            };

            var strategy = new StandardRankingStrategy();

            var standings = strategy.CalculateStandings(teams, matches).ToList();

            Assert.Equal(3, standings.Count);
            Assert.Equal(1, standings[0].TeamId); // better goal difference
            Assert.Equal(2, standings[1].TeamId);
        }
    }
}
