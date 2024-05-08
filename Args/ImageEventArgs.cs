using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Args
{
    public class ImageEventArgs : EventArgs
    {
        public string label {  get; set; }
        public Bitmap Image { get; set; }

        public ImageEventArgs(string label, Bitmap image)
        {
            this.label = label;
            Image = image;
        }
    }
}
