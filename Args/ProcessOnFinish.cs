using EmguClass.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Args
{
    public class ProcessOnFinishEventArgs : EventArgs
    {
        public ResultType ResultType { get; set; } = ResultType.Fail;

        public ProcessOnFinishEventArgs(ResultType Result) 
        {
            ResultType = Result;
        }
    }
}
