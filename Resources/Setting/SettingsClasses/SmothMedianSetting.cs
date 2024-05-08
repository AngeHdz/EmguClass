using EmguClass.Resources.Setting.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguClass.Resources.Setting.SettingsClasses
{
    public class SmothMedianSetting : ISettings
    {
        public int size = 0;
        public SmothMedianSetting(int size)
        {
            this.size = size;
        }   
    }
}
