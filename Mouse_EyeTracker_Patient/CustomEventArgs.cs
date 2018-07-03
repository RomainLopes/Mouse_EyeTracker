using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouse_EyeTracker_Patient
{
    public class CustomEventArgs : EventArgs
    {
        private int i;

        public CustomEventArgs(int i)
        {
            this.i = i;
        }

        public int IntValue
        {
            get { return i; }
            set { i = value; }
        }
    }
}
