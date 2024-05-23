using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Dictionary
{
    public interface IProcessImage
    {
        Image<Bgr, byte> Process(Image<Bgr, byte> image);
    }
}
