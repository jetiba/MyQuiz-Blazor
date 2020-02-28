using System.Collections.Generic;
using System.Threading.Tasks;
using quiz.shared;

namespace quiz.client.Services
{
    public interface ILeaderboardService
    {
        Task<List<Leaderboard>> GetAllPoints();
        Task<int> GetPointsByUser(string username);
        Task<bool> AddPoints(Leaderboard leaderboard);
    }
}