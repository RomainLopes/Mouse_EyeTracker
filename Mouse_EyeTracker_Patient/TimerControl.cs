using System;

namespace Mouse_EyeTracker_Patient
{

    public class TimerControl
    {
        #region Variables

        public int timeMsTimer { get; set; } = 2000;
        public void setTimeMsTimer(int i)
        {
            this.timeMsTimer = i;
        }
        private  int timeMsUpdateProgressBar = 50;
        private  int nbUpdateProgressBar = 40; // int = timeMsTimers / timeMsUpdateProgressBar
        private  int countUpdateProgressBar = 0;

        private System.Timers.Timer timerCPB1;
        private System.Timers.Timer timerCPB2;
        private System.Timers.Timer timerCPB3;
        private System.Timers.Timer timerCPB4;

        #endregion

        public TimerControl()
        {
            timerCPB1 = new System.Timers.Timer();
            timerCPB1.Interval = timeMsUpdateProgressBar;
            timerCPB1.AutoReset = true;

            timerCPB2 = new System.Timers.Timer();
            timerCPB2.Interval = timeMsUpdateProgressBar;
            timerCPB2.AutoReset = true;

            timerCPB3 = new System.Timers.Timer();
            timerCPB3.Interval = timeMsUpdateProgressBar;
            timerCPB3.AutoReset = true;

            timerCPB4 = new System.Timers.Timer();
            timerCPB4.Interval = timeMsUpdateProgressBar;
            timerCPB4.AutoReset = true;

            timerCPB1.Elapsed += (sender, e) => OntimerCPB1Event(sender, e);
            timerCPB2.Elapsed += (sender, e) => OntimerCPB2Event(sender, e);
            timerCPB3.Elapsed += (sender, e) => OntimerCPB3Event(sender, e);
            timerCPB4.Elapsed += (sender, e) => OntimerCPB4Event(sender, e);
        }

        #region Timer's events
        public delegate void CustomEventHandler(object sender, CustomEventArgs a);

        public CustomEventHandler TimerCPB1Update;
        protected virtual void OnTimerCPB1Update(CustomEventArgs e)
        {
            if (TimerCPB1Update != null)
                TimerCPB1Update(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTimerCPB1Update");
            }
        }

        public CustomEventHandler TimerCPB2Update;
        protected virtual void OnTimerCPB2Update(CustomEventArgs e)
        {
            if (TimerCPB2Update != null)
                TimerCPB2Update(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTimerCPB2Update");
            }
        }

        public CustomEventHandler TimerCPB3Update;
        protected virtual void OnTimerCPB3Update(CustomEventArgs e)
        {
            if (TimerCPB3Update != null)
                TimerCPB3Update(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTimerCPB3Update");
            }
        }

        public CustomEventHandler TimerCPB4Update;
        protected virtual void OnTimerCPB4Update(CustomEventArgs e)
        {
            if (TimerCPB4Update != null)
                TimerCPB4Update(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTimerCPB4Update");
            }
        }

        public EventHandler TimerCPB1IsUp;
        protected virtual void OnTimerCPB1IsUp(EventArgs e)
        {
            if (TimerCPB1IsUp != null)
                TimerCPB1IsUp(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTimerCPB1IsUp");
            }
        }

        public EventHandler TimerCPB2IsUp;
        protected virtual void OnTimerCPB2IsUp(EventArgs e)
        {
            if (TimerCPB2IsUp != null)
                TimerCPB2IsUp(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTimerCPB2IsUp");
            }
        }

        public EventHandler TimerCPB3IsUp;
        protected virtual void OnTimerCPB3IsUp(EventArgs e)
        {
            if (TimerCPB3IsUp != null)
                TimerCPB3IsUp(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTimerCPB3IsUp");
            }
        }

        public EventHandler TimerCPB4IsUp;
        protected virtual void OnTimerCPB4IsUp(EventArgs e)
        {
            if (TimerCPB4IsUp != null)
                TimerCPB4IsUp(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTimerCPB4IsUp");
            }
        }

        #endregion


        

        #region timer's event
        public void OntimerCPB1Event(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (countUpdateProgressBar < nbUpdateProgressBar)
            {
                countUpdateProgressBar += 1;

                OnTimerCPB1Update(new CustomEventArgs(countUpdateProgressBar * timeMsUpdateProgressBar));
            }
            else
            {
                countUpdateProgressBar = 0;
                timerCPB1.Dispose();

                OnTimerCPB1IsUp(new EventArgs());
            }
        }

        public void OntimerCPB2Event(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (countUpdateProgressBar < nbUpdateProgressBar)
            {
                countUpdateProgressBar += 1;

                OnTimerCPB2Update(new CustomEventArgs(countUpdateProgressBar * timeMsUpdateProgressBar));
            }
            else
            {
                countUpdateProgressBar = 0;
                timerCPB2.Close();

                OnTimerCPB2IsUp(new EventArgs());
            }
        }

        public void OntimerCPB3Event(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (countUpdateProgressBar < nbUpdateProgressBar)
            {
                countUpdateProgressBar += 1;

                OnTimerCPB3Update(new CustomEventArgs(countUpdateProgressBar * timeMsUpdateProgressBar));
            }
            else
            {
                countUpdateProgressBar = 0;
                timerCPB3.Close();

                OnTimerCPB3IsUp(new EventArgs());
            }
        }

        public void OntimerCPB4Event(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (countUpdateProgressBar < nbUpdateProgressBar)
            {
                countUpdateProgressBar += 1;

                OnTimerCPB4Update(new CustomEventArgs(countUpdateProgressBar * timeMsUpdateProgressBar));
            }
            else
            {
                countUpdateProgressBar = 0;
                timerCPB4.Close();

                OnTimerCPB4IsUp(new EventArgs());
            }
        }
        #endregion

        #region timer's start
        public void timerCPB1Start()
        {
            timerCPB1.Start();
        }

        public void timerCPB2Start()
        {
            timerCPB2.Start();
        }
        public void timerCPB3Start()
        {
            timerCPB3.Start();
        }
        public void timerCPB4Start()
        {
            timerCPB4.Start();
        }
        #endregion

        #region timer's close
        public void timerCPB1Close()
        {
            if(countUpdateProgressBar < nbUpdateProgressBar)
            {
                countUpdateProgressBar = 0;

                OnTimerCPB1Update(new CustomEventArgs(0));
                timerCPB1.Close();
            }
            countUpdateProgressBar = 0;
            timerCPB1.Close();
        }

        public void timerCPB2Close()
        {
            if (countUpdateProgressBar < nbUpdateProgressBar)
            {
                countUpdateProgressBar = 0;

                OnTimerCPB2Update(new CustomEventArgs(0));
                timerCPB2.Close();
            }
            countUpdateProgressBar = 0;
            timerCPB2.Close();
        }

        public void timerCPB3Close()
        {
            if (countUpdateProgressBar < nbUpdateProgressBar)
            {
                countUpdateProgressBar = 0;

                OnTimerCPB3Update(new CustomEventArgs(0));
                timerCPB3.Close();
            }
            countUpdateProgressBar = 0;
            timerCPB3.Close();
        }

        public void timerCPB4Close()
        {
            if (countUpdateProgressBar < nbUpdateProgressBar)
            {
                countUpdateProgressBar = 0;

                OnTimerCPB4Update(new CustomEventArgs(0));
                timerCPB4.Close();
            }
            countUpdateProgressBar = 0;
            timerCPB4.Close();
        }
        #endregion

    }
}
