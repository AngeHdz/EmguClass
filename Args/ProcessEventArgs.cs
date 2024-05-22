using EmguClass.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Args
{
    public class ProcessEventArgs : EventArgs
    {
        public ResultClass _result;

        public ProcessEventArgs(ResultClass result) 
        {
            _result = result;
        }
    }
}
