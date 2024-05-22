using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Results.Types
{
    public class StringResult : ResultClass
    {
        public string Meas {  get; set; }
        public string Limit { get; set; }
        public StringResult(int id, string name,string limit, string meas, string description = "") : base(id, name, description)
        {
            Limit = limit;
            Meas = meas;
            TestResultType = TestResultType.String;
            Validate();
        }

        public override void Validate()
        {
            if (Limit == Meas)
            {
                ResultType = ResultType.Pass;
            }
        }

        public override string[] ResultInfo()
        {
            return new string[] { Id.ToString(), Name, Limit, Meas.ToString(), Limit, ResultType.ToString(), TestResultType.ToString() };
        }
    }
}
