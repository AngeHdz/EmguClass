using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Resources.Setting
{
    public class Settings
    {
        public int Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int KernelSize { get; set; }

        public int kernelSize {  get; set; }
        public int colorSigma {  get; set; }
        public int spaceSigma { get; set; }

        public int xorder { get; set; }
        public int yorder { get; set; }
        public int apertureSize { get; set; }

        public double tresh { get; set; }
        public double treshLinking { get; set; }
        public bool I2Gradient {  get; set; }

        public TypeProcess Type { get; set; } = TypeProcess.None;

        //public Settings(int size)
        //{
        //    Size = size;
        //}

        public Settings() { }
    }
}
