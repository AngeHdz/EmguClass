using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Tools
{
    public enum TestType 
    {
        Hvac_Buttons,
        Hvac_Leds
    }
    public class Test
    {
        public Rectangle Rectangle { get; set; }
        public Image<Gray,byte> ROIMask { get; set; }

        public Image<Bgr, byte> Roi { get; set; }

        public Image<Gray, byte> ROIMaskReduce { get; set; }

        public Image<Bgr, byte> RoiReduce { get; set; }
        public Image<Bgr, byte> Item { get; set; }

        public String TestName { get; set; }

        public Test(string testName,Rectangle rectangle)
        {
            TestName = testName;
            Rectangle = rectangle;
        }

        public static List<Test> GetTest(TestType testType) 
        {
            List<Test> list = new List<Test>();
            switch (testType) 
            {
                case TestType.Hvac_Buttons:
                    list.Add(new Test("D25", new Rectangle(1200, 650, 400, 500)));
                    list.Add(new Test("D20", new Rectangle(1200, 1250, 400, 500)));
                    list.Add(new Test("D21", new Rectangle(1650, 1250, 400, 500)));
                    list.Add(new Test("D22", new Rectangle(2100, 1250, 400, 500)));
                    list.Add(new Test("D23", new Rectangle(2550, 1250, 400, 500)));
                    list.Add(new Test("D24", new Rectangle(3000, 1250, 400, 500)));
                    list.Add(new Test("D26", new Rectangle(3000, 650, 400, 500)));
                    break;
                    default: 
                    break;
            }

            return list;
        }
    }
}
