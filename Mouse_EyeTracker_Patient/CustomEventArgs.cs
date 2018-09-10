using System;

namespace Mouse_EyeTracker_Patient
{
    public class CustomEventArgs : EventArgs
    {
        private int i;
        private Boolean value;

        public CustomEventArgs(int i)
        {
            this.i = i;
        }

        public int IntValue
        {
            get { return i; }
            set { i = value; }
        }

        public Boolean BooleanValue
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public CustomEventArgs(Boolean value)
        {
            this.value = value;
        }
    }
}
