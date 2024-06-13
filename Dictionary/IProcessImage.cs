using Emgu.CV;
using Emgu.CV.Structure;
using EmguClass.Resources.Setting;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    public class SmoothBilateralProcess : IProcessImage
    {
        public void Process(Image<Bgr, byte> InputImage, ref Image<Bgr, byte> OutputImage, Settings Data)
        {
            OutputImage = InputImage.SmoothBilateral(Data.kernelSize, Data.colorSigma, Data.spaceSigma);
        }
    }

    public class ErodeProcess : IProcessImage
    {
        public void Process(Image<Bgr, byte> InputImage, ref Image<Bgr, byte> OutputImage, Settings Data)
        {
            OutputImage = InputImage.Erode(Data.kernelSize);
        }
    }

    public class DilateProcess : IProcessImage
    {
        public void Process(Image<Bgr, byte> InputImage, ref Image<Bgr, byte> OutputImage, Settings Data)
        {
            OutputImage = InputImage.Dilate(Data.kernelSize);
        }
    }

    public class SobelProcess : IProcessImage
    {
        public void Process(Image<Bgr, byte> InputImage, ref Image<Bgr, byte> OutputImage, Settings Data)
        {
            Image<Bgr, float> res = InputImage.Sobel(Data.xorder, Data.yorder, Data.apertureSize);
            OutputImage = Functions.GlobalFunctions.ConvertFloatToByte(res);
        }
    }

    public class LedAmbarProcess : IProcessImage
    {
        public void Process(Image<Bgr, byte> InputImage, ref Image<Bgr, byte> OutputImage, Settings Data)
        {
            OutputImage = VisionClass.Ambar_DarkBackground(InputImage);
        }
    }

    public class Cannyprocess : IProcessImage
    {
        public void Process(Image<Bgr, byte> InputImage, ref Image<Bgr, byte> OutputImage, Settings Data)
        {
            Image<Gray, byte> result = InputImage.Canny(Data.tresh, Data.treshLinking,Data.apertureSize, Data.I2Gradient);
            OutputImage = result.Convert<Bgr, byte>();
        }
    }

    //public class FlipProcess : IProcessImage
    //{
    //    public void Process(Image<Bgr, byte> InputImage, ref Image<Bgr, byte> OutputImage, Settings Data)
    //    {
    //        OutputImage = InputImage.Flip(Emgu.CV.CvEnum.FlipType fliptype)
    //    }
    //}

    //public class DilateProcess : IProcessImage
    //{
    //    public void Process(Image<Bgr, byte> InputImage, ref Image<Bgr, byte> OutputImage, Settings Data)
    //    {
    //        OutputImage = InputImage.
    //    }
    //}
}
