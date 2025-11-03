using System;
using System.ComponentModel.DataAnnotations;

namespace TeamRanker.Api.Models
{

    public class MatchDto
    {
        public int Id { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        public string? HomeTeamName { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        public string? AwayTeamName { get; set; }

        [Range(0, int.MaxValue)]
        public int HomeScore { get; set; }

        [Range(0, int.MaxValue)]
        public int AwayScore { get; set; }

        public DateTime PlayedOn { get; set; }
    }
}
