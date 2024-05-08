using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Results.Types
{
    public class NumericResult : Result
    {
        public double LowLimit {  get; set; }
        public double HighLimit { get; set; }
        public double Meas { get; set; }

        public NumericResult(int id, string name, double lowLimit, double highLimit, double meas, string description = "") : base(id, name, description)
        {
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Meas = meas;
            TestResultType = TestResultType.Numeric;
            Validate();
        }

        public override void Validate()
        {
            if (Meas >= LowLimit && Meas <= HighLimit)
            {
                ResultType = ResultType.Pass;
            }
        }

        public override string[] ResultInfo()
        {
            return new string[] { Id.ToString(), Name, LowLimit.ToString(), Meas.ToString(), HighLimit.ToString(), ResultType.ToString(), TestResultType.ToString()};
        }
    }
}
