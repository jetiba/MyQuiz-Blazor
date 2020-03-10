using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace quiz.client.Services
{
    public interface IQuestionService
    {
        void GetTimer(ref Timer timer);
        int GetTime();
        Task<List<Question>> GetQuestions();
        Task<bool> AddQuestions(List<Question> questionsList);
        int GetQuestionNumber();

    }
}