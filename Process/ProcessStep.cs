using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Process
{
    public class ProcessStep
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public ProcessStep(string name, string type)
        {
            Name = name;
            this.Type = type;
        }
    }
}
