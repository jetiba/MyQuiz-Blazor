using Microsoft.EntityFrameworkCore;
using quiz.shared;

namespace quiz.server.Data
{
    public class LeaderboardDbContext : DbContext
     {
        public DbSet<Leaderboard> Leaderboards { get; set; }

        public LeaderboardDbContext(DbContextOptions<LeaderboardDbContext> options) : base(options)
        {
        }
     }

}
