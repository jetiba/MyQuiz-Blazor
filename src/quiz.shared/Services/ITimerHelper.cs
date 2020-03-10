using System.Timers;

namespace quiz.shared.Services
{
    public interface ITimerHelper
    {
        Timer GetTimer();
        int GetTime();
    }
}