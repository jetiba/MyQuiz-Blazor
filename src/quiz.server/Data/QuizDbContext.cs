using Microsoft.EntityFrameworkCore;
using quiz.shared;

namespace quiz.server.Data
{
    public class QuizDbContext : DbContext
     {
        public DbSet<Leaderboard> Leaderboards { get; set; }

        public DbSet<Question> Questions { get; set; }
        
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options)
        {
        }
     }

}
