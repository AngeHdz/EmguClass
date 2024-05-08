using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Results
{
    public abstract class Result
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TestResultType TestResultType { get; set; } = TestResultType.None;
        public ResultType ResultType { get; set; } = ResultType.Fail;
        public abstract void Validate();
        public abstract string[] ResultInfo();


        public Result(int id,string name, string description = "")
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
