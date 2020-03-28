

using System.ComponentModel.DataAnnotations;

namespace quiz.shared
{
    public class Leaderboard
    {
        [Key]
        public string Username { get; set; }
        
        public int Points { get; set; }

        public int GamePlayed { get; set; }

        public bool HasPlayedLastGame { get; set; }
    }
}