using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace quiz.client.Services
{
    public interface IQuestionService
    {
        Task<Timer> GetTimer();
        int GetTime();
        Task<List<Question>> GetQuestions();
        Task<bool> AddQuestions(List<Question> questionsList);
        int GetQuestionNumber();

    }
}