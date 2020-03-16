using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quiz.server.Data;

namespace quiz.server.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        private readonly QuizDbContext ldbcontext;

        public QuestionController(QuizDbContext ldbcontext)
        {
            this.ldbcontext = ldbcontext;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetQuestions()
        {
            var p = await ldbcontext.Questions.AsNoTracking().ToListAsync();
            
            return new JsonResult(p);
           
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddQuestions([FromBody]List<Question> questionsList)
        {
            if(questionsList.Count > 0)
            {
                foreach(var q in questionsList){
                    await ldbcontext.Questions.AddAsync(q);
                    
                }
                await ldbcontext.SaveChangesAsync();

                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}