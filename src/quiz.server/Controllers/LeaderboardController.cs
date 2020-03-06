using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quiz.server.Data;
using quiz.shared;

namespace quiz.server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class LeaderboardController : Controller
    {
        private readonly LeaderboardDbContext ldbcontext;

        public LeaderboardController(LeaderboardDbContext ldbcontext)
        {
            this.ldbcontext = ldbcontext;
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPointsByUser(string username)
        {
            var p = await ldbcontext.Leaderboards.AsNoTracking().Where(x => string.Equals(x.Username, username)).FirstOrDefaultAsync();
            if(p != default){
                return new JsonResult(p);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPoints()
        {
            var p = await ldbcontext.Leaderboards.AsNoTracking().OrderByDescending(x => x.Points).ToListAsync();
            
            return new JsonResult(p);
           
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddPoints([FromBody]Leaderboard leaderboard)
        {
            if(string.Equals(User.Identity.Name, leaderboard.Username))
            {
                var old = await ldbcontext.Leaderboards.FindAsync(leaderboard.Username);
                if(old == default)
                {
                    await ldbcontext.Leaderboards.AddAsync(leaderboard);
                }
                else
                {
                    old.Points += leaderboard.Points;
                }
                
                await ldbcontext.SaveChangesAsync();

                return new OkResult();
            }
            
            return new UnauthorizedResult();
        }
    }
}