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
    public static class EmguFunctions
    {
        public static Image<Bgr, byte> GetProcess(Image<Bgr, byte> Image,Settings Data) 
        {
            var actions = new Dictionary<Predicate<TypeProcess>, IProcessImage>
            {
                { t => t == TypeProcess.SmoothMedian, new SmoothMedianProcess() },
                { t => t == TypeProcess.SmoothBlur, new SmoothBlurProcess() },
                { t => t == TypeProcess.SmoothGaussian, new GaussianProcess() }
            };

            var operation = actions.ToList().Find(d => d.Key(Data.Type)).Value;
            Image<Bgr,byte>? ResultImage = null;
            operation.Process(Image,ref ResultImage, Data);
            return ResultImage;
        }
    }

    
}
