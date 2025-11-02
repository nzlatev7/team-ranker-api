using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamRanker.Core.Entities;

public class Match
{
    public int Id { get; set; }

    [Required]
    public int HomeTeamId { get; set; }

    [ForeignKey(nameof(HomeTeamId))]
    public Team? HomeTeam { get; set; }

    [Required]
    public int AwayTeamId { get; set; }

    [ForeignKey(nameof(AwayTeamId))]
    public Team? AwayTeam { get; set; }

    [Range(0, int.MaxValue)]
    public int HomeScore { get; set; }

    [Range(0, int.MaxValue)]
    public int AwayScore { get; set; }

    public DateTime PlayedOn { get; set; }
}
