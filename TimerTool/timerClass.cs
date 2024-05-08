using EmguClass.Args;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.TimerTool
{
    public class timerClass 
    {
        private System.Timers.Timer timer;
        private TimeSpan timerspan;
        Stopwatch stopwatch = new Stopwatch();
        public event EventHandler<TimerEventArgs> OnReportTime;

        public timerClass()
        {
            timer = new System.Timers.Timer(100);
            timer.Elapsed += Timer_Elapsed;

        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            timerspan = stopwatch.Elapsed;
            OnReportImageReached(new TimerEventArgs(timerspan.ToString(@"hh\:mm\:ss\:fff")));
        }

        public void Start() 
        {
            timer.Enabled = true;
            stopwatch.Restart();
        }
        public void Stop()
        {
            timer.Enabled = false;
            stopwatch.Stop();
        }

        protected virtual void OnReportImageReached(TimerEventArgs e)
        {
            OnReportTime.Invoke(this, e);
        }
    }
}
