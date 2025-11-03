using System;
using System.ComponentModel.DataAnnotations;

namespace TeamRanker.Api.Models
{

    public class MatchRequest
    {
        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [Range(0, int.MaxValue)]
        public int HomeScore { get; set; }

        [Range(0, int.MaxValue)]
        public int AwayScore { get; set; }

        [Required]
        public DateTime PlayedOn { get; set; }
    }
}
