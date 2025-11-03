using System.ComponentModel.DataAnnotations;

namespace TeamRanker.Api.Models;

public class TeamDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? City { get; set; }
}
