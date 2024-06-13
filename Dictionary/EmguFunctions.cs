using Emgu.CV;
using Emgu.CV.Structure;
using EmguClass.Resources.Setting;

namespace EmguClass.Dictionary
{
    public static class EmguFunctions
    {
        public static Image<Bgr, byte> GetProcess(Image<Bgr, byte> Image,Settings Data) 
        {
            var actions = new Dictionary<Predicate<TypeProcess>, IProcessImage>
            {
                { t => t == TypeProcess.SmoothMedian, new SmoothMedianProcess() },
                { t => t == TypeProcess.SmoothBlur, new SmoothBlurProcess() },
                { t => t == TypeProcess.SmoothGaussian, new GaussianProcess() },
                { t => t == TypeProcess.SmoothBilateral, new SmoothBilateralProcess() },
                { t => t == TypeProcess.Erode, new ErodeProcess() },
                { t => t == TypeProcess.Dilate, new DilateProcess() },
                { t => t == TypeProcess.Led_Ambar, new LedAmbarProcess() },
                { t => t == TypeProcess.Canny, new Cannyprocess() }
            };

            var operation = actions.ToList().Find(d => d.Key(Data.Type)).Value;
            Image<Bgr,byte>? ResultImage = null;
            operation.Process(Image,ref ResultImage, Data);
            return ResultImage;
        }
    }
}
