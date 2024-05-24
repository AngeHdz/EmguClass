using Emgu.CV;
using Emgu.CV.Structure;
using EmguClass.Resources.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Dictionary
{
    public interface IProcessImage
    {
        void Process(Image<Bgr, byte> InputImage, ref Image<Bgr, byte> OutputImage, Settings Data);
    }


    public class SmoothMedianProcess : IProcessImage
    {
        public void Process(Image<Bgr, byte> InputImage, ref Image<Bgr, byte> OutputImage, Settings Data)
        {
            OutputImage = InputImage.SmoothMedian(Data.Size);
        }
    }

    public class SmoothBlurProcess: IProcessImage
    {
        public void Process(Image<Bgr,byte> InputImage, ref Image<Bgr,byte> OutputImage, Settings Data)
        {
            OutputImage = InputImage.SmoothBlur(Data.Width, Data.Height);
        }
    }
    public class GaussianProcess : IProcessImage
    {
        public void Process(Image<Bgr, byte> InputImage, ref Image<Bgr, byte> OutputImage, Settings Data)
        {
            OutputImage = InputImage.SmoothGaussian(Data.KernelSize);
        }
    }
}
