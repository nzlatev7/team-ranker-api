using Microsoft.EntityFrameworkCore;
using TeamRanker.Core.Entities;

namespace TeamRanker.Api.Data
{

    public class TeamRankerDbContext : DbContext
    {
        public TeamRankerDbContext(DbContextOptions<TeamRankerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Team> Teams => Set<Team>();

        public DbSet<Match> Matches => Set<Match>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasMany(t => t.HomeMatches)
                    .WithOne(m => m.HomeTeam!)
                    .HasForeignKey(m => m.HomeTeamId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(t => t.AwayMatches)
                    .WithOne(m => m.AwayTeam!)
                    .HasForeignKey(m => m.AwayTeamId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasIndex(m => new { m.HomeTeamId, m.AwayTeamId, m.PlayedOn });
            });
        }
    }
}
