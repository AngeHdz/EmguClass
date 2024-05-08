using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Args
{
    public class TimerEventArgs : EventArgs
    {
        public string time {  get; set; }

        public TimerEventArgs(string time)
        {
            this.time = time;
        }
    }
}
