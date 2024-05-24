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

        public TypeProcess Type { get; set; } = TypeProcess.None;

        public Settings(int size)
        {
            Size = size;
        }

        public Settings() { }
    }
}
