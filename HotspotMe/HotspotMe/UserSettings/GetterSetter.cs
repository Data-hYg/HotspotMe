using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotspotMe.UserSettings
{
    public class GetterSetter
    {
        int networkIndex;
        bool _save;
        bool _cancel = false;

        public int networkindex
        {
            get { return HotspotMe.Properties.Settings.Default.networkIndex; }
            set { networkIndex = value; HotspotMe.Properties.Settings.Default.networkIndex = value; }
        }
        public bool save
        {
            get { return _save; }
            set { _save = value; }
        }
        
        public bool cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }
    }
}
