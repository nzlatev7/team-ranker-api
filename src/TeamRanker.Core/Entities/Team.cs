using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamRanker.Core.Entities
{

    public class Team
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? City { get; set; }

        public ICollection<Match> HomeMatches { get; set; } = new List<Match>();

        public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
    }
}
