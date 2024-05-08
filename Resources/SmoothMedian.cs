using Emgu.CV;
using Emgu.CV.Structure;
using EmguClass.Resources.Setting.Interface;
using EmguClass.Resources.Setting.SettingsClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Resources
{
    public class SmoothMedian : IProcessImage
    {
        public ProcessImageTypes ProcessImageTypes = ProcessImageTypes.SmootMedian;
        public string Name { get; set; }

        public SmothMedianSetting Setting;

        public SmoothMedian(string name, ISettings setting)
        {
            Name = name;
            Setting = (SmothMedianSetting)setting;
        }
        public Bitmap execute(Bitmap Image)
        {
            try
            {
                Image<Hsv, byte> NewImage = Image.ToImage<Hsv, byte>();
                
                NewImage = NewImage.SmoothMedian(Setting.size);
                return NewImage.ToBitmap();
            }
            catch (Exception e) 
            {
                return null;
            }
        }


        public string GetTitle()
        {
            return Name;
        }
    }
}
