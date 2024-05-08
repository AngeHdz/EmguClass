using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Tools
{
    public enum ColorType
    {
        ambar,
        red,
        green,
        blue,
        Black,
        White
    }

    public class ColorE 
    {
        public Hsv Low {  get; set; }
        public Hsv High {  get; set; }

        public ColorE() { }

        public static ColorE GetLimits(ColorType colorType) 
        {
            ColorE c = new ColorE();
            switch (colorType)
            {
                case ColorType.ambar:
                    c.Low = new Hsv(10, 50, 50);
                    c.High = new Hsv(40, 255, 255);
                    break;
                default:
                    break;
            }
            return c;
        }
    }
}
