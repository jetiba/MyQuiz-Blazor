using System.Timers;

namespace quiz.server.Services
{
    public interface ITimerHelper
    {
        Timer GetTimer();
        int GetTime();
    }
}