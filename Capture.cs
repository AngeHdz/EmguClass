using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass
{
    public static class Capture
    {
        public static Bitmap capture()
        {
            return VisionClass.capture().ToBitmap();
        }
    }
}
