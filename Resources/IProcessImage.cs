using Emgu.CV;
using Emgu.CV.Structure;
using EmguClass.Resources.Setting.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Resources
{
    public enum ProcessImageTypes
    {
        SmootMedian,
        Canny,
        Led_Ambar
    }
    public interface IProcessImage
    {
        Bitmap execute(Bitmap Image);
        public string GetTitle();
    }

    public static class ProcessImage
    {
        public static IProcessImage Crear(ProcessImageTypes type, Bitmap Image, string Name, ISettings data)
        {
            switch (type)
            {
                case ProcessImageTypes.SmootMedian:
                    return new SmoothMedian(Name, data);
                    break;
            }
            return null;
        }
    }
}
