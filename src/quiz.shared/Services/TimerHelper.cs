
using System;
using System.Timers;

namespace quiz.shared.Services
{
    public class TimerHelper : ITimerHelper
    {
        private readonly Timer aTimer;
        private int time;

        public TimerHelper()
        {
            aTimer = new System.Timers.Timer(1000);
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            aTimer.Elapsed += OnTimedEvent;
            time = 10;
        }

        public Timer GetTimer()
        {
            return aTimer;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if(time > 0)
            {
                time--;       
            }
            else
            {
                time = 10;
            }
        }

        public int GetTime(){
            return time;
        }
    }
}
