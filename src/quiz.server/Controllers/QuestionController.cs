using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using quiz.server.Data;

namespace quiz.server.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        private readonly QuizDbContext ldbcontext;
        private TelemetryClient telemetryClient;

        public QuestionController(QuizDbContext ldbcontext, TelemetryClient telemetryClient)
        {
            this.ldbcontext = ldbcontext;
            this.telemetryClient = telemetryClient;
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
                try
                {
                    await ldbcontext.SaveChangesAsync();
                }
                catch(Exception e)
                {
                    telemetryClient.TrackException(e,
                                           new Dictionary<string, string>() { 
                                               { "operation", "upload" },
                                               { "questions", JsonConvert.SerializeObject(questionsList) }, 
                                               { "user", User.Identity.Name }});
                }

                telemetryClient.TrackEvent("questionsuploaded",
                                           new Dictionary<string, string>() { 
                                               { "questions", JsonConvert.SerializeObject(questionsList) },
                                               { "user", User.Identity.Name }});

                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }

        [HttpDelete("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteQuestions()
        {
            var qdata = await ldbcontext.Questions.AsNoTracking().ToListAsync();
            if(qdata.Count > 0)
            {
                ldbcontext.Questions.RemoveRange(qdata);
                await ldbcontext.SaveChangesAsync();

                var leaderboard = await ldbcontext.Leaderboards.ToListAsync();
                foreach(var l in leaderboard)
                {
                    l.HasPlayedLastGame = false;
                }

                ldbcontext.Leaderboards.UpdateRange(leaderboard);

                try
                {
                    await ldbcontext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    telemetryClient.TrackException(e,
                                           new Dictionary<string, string>() { 
                                               { "operation", "delete" }, 
                                               { "user", User.Identity.Name }});
                }
                
                telemetryClient.TrackEvent("questionsdeleted",
                                           new Dictionary<string, string>() { 
                                               { "user", User.Identity.Name }});
                
                return new OkResult();
            }
            else
            {
                return new NotFoundResult();
            }            
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTemplateFile()
        {
            var path = Path.Combine(
                            Directory.GetCurrentDirectory(), 
                            "StaticFiles", 
                            "Template.csv"
                        );
            var data = await System.IO.File.ReadAllTextAsync(path);
            return new OkObjectResult(data);
        }
    }
}