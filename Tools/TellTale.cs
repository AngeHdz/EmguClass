using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Tools
{
    public class TellTale
    {
        public string Name { get; set; }

        public string TestName { get; set; }
        public Image<Bgr, byte> Template { get; set; }

        public TellTale(string name,string testName,string path) 
        {
            this.Name = name;
            this.TestName = testName;
            this.Template = new Image<Bgr, byte>($"{path}{name}.bmp");
        }

        public static List<TellTale> getTelltales(string path)
        {
            List<TellTale> d = new List<TellTale>();
            d.Add(new TellTale("OnOff", "D25", path));
            d.Add(new TellTale("Auto","D20" ,path));
            d.Add(new TellTale("Cold","D21", path));
            d.Add(new TellTale("ColdMax", "D22", path));
            d.Add(new TellTale("MaxDefrost", "D23", path));
            d.Add(new TellTale("FanDown", "D24", path));
            d.Add(new TellTale("FanUp", "D26", path));


            return d;
        } 

    }
}
