using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Results.Types
{
    public class BooleanResult :ResultClass
    {
        public bool Meas {  get; set; }
        public BooleanResult(int id,string name,bool meas, string description = ""): base(id,name, description) 
        {
            Meas = meas;
            TestResultType = TestResultType.Boolan;
            Validate();
        }

        public override void Validate()
        {
            if(Meas) 
            {
                ResultType = ResultType.Pass;
            }
        }

        public override string[] ResultInfo()
        {
            return new string[] { Id.ToString(), Name, "True", Meas.ToString(), "True", ResultType.ToString(), TestResultType.ToString() };
        }
    }
}
