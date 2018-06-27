using System;
//using namespaceProgram;

namespace Mouse_EyeTracker_Patient
{
    /// <summary>
    /// Timer's management methods, each timer get a public boolean that inform if the timer is running
    /// </summary>
    public class TimerBlinkManagement
    {
        #region boolean data on is XXX timer's running
        private bool isVerifyingBlinkTimer = false;
        private bool isFirstBlinkTimer = false;
       

        public bool getIsVerifyingBlinkTimer()
        {
            return isVerifyingBlinkTimer;
        }
        public bool getIsFirstBlinkTimer()
        {
            return isFirstBlinkTimer;
        }


        public void setIsVerifyingBlinkTimer(Boolean value)
        {
            isVerifyingBlinkTimer = value;
        }
        public void setIsFirstBlinkTimer(Boolean value)
        {
            isFirstBlinkTimer = value;
        }


        #endregion

            #region timers instanciation
        private static System.Timers.Timer verifyingBlinkTimer;
        private static System.Timers.Timer firstBlinkTimer;
        #endregion

        /// <summary>
        /// enumeration of the differents timer
        /// </summary>
        public enum TimersAvailable
        {
            verifyingBlinkTimer,
            firstBlinkTimer,
        }


        public EventHandler VerifyingTimerIsUp;
        public EventHandler FirstTimerIsUp;
        public EventHandler AllowingCursorToMove;

        protected virtual void OnAllowingCursorToMove(EventArgs e)
        {
            if (AllowingCursorToMove != null)
                AllowingCursorToMove(this, e);
            else
            {
                Console.WriteLine("NullPointer OnAllowingCursorToMove");
            }
        }
        protected virtual void OnVerifyingTimerIsUp(EventArgs e)
        {
            if (VerifyingTimerIsUp != null)
                VerifyingTimerIsUp(this, e);
            else
            {
                Console.WriteLine("NullPointer OnVerifyingTimerIsUp");
            }
        }

        protected virtual void OnFirstTimerIsUp(EventArgs e)
        {
            if (FirstTimerIsUp != null)
                FirstTimerIsUp(this, e);
            else
            {
                Console.WriteLine("NullPointer OnFirstTimerIsUp");
            }
        }

       

        /// <summary>
        /// Construct that initialize the timers
        /// Set up the interval of the timer, their autoreset to false and the specialized event they're calling
        /// </summary>
        public TimerBlinkManagement()
        {
            verifyingBlinkTimer = new System.Timers.Timer();
            verifyingBlinkTimer.Interval = 1300;
            verifyingBlinkTimer.Elapsed += (sender, e) => OnverifyingBlinkTimerEvent(sender, e);
            verifyingBlinkTimer.AutoReset = false;

            firstBlinkTimer = new System.Timers.Timer();
            firstBlinkTimer.Interval = 2500;
            firstBlinkTimer.Elapsed += (sender, e) => OnfirstBlinkTimerEvent(sender, e);
            firstBlinkTimer.AutoReset = false;

        }


        /// <summary>
        /// Start any timer named in the enumeration, and set the boolean value to true
        /// </summary>
        /// <param name="timerName"></param>
        public void StartTimer(TimersAvailable timerName)
        {
            switch (timerName)
            {
                case TimersAvailable.verifyingBlinkTimer:
                    isVerifyingBlinkTimer = true;
                    verifyingBlinkTimer.Start();
                    break;
                case TimersAvailable.firstBlinkTimer:
                    isFirstBlinkTimer = true;
                    firstBlinkTimer.Start();
                    break;
                default:
                    throw new ArgumentException("Unhandled timer: " + timerName);
            }
        }

        /// <summary>
        /// Stop any timer named in the enumeration, and set the boolean value to false
        /// </summary>
        /// <param name="timerName"></param>
        public void StopTimer(TimersAvailable timerName)
        {
            switch (timerName)
            {
                case TimersAvailable.verifyingBlinkTimer:
                    isVerifyingBlinkTimer = false;
                    verifyingBlinkTimer.Stop();
                    OnAllowingCursorToMove(new EventArgs());
                   // mmgt.setIsCursorAllowedToMove(true); //---------------------------------------------
                    break;
                case TimersAvailable.firstBlinkTimer:
                    isFirstBlinkTimer = false;
                    firstBlinkTimer.Stop();
                    break;
                default:
                    throw new ArgumentException("Unhandled timer: " + timerName);
            }
        }

        /// <summary>
        /// Close any timer named in the enumeration, and set the boolean value to false
        /// </summary>
        /// <param name="timerName"></param>
        public void CloseTimer(TimersAvailable timerName)
        {
            switch (timerName)
            {
                case TimersAvailable.verifyingBlinkTimer:
                    isVerifyingBlinkTimer = false;
                    verifyingBlinkTimer.Close();
                    break;
                case TimersAvailable.firstBlinkTimer:
                    isFirstBlinkTimer = false;
                    firstBlinkTimer.Close();
                    break;
                default:
                    throw new ArgumentException("Unhandled timer: " + timerName);
            }
        }

        /// <summary>
        /// Event called when verifying timer is finished, close timer, and allowMouseToMove if 
        /// first blink timer is not running
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnverifyingBlinkTimerEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            CloseTimer(TimersAvailable.verifyingBlinkTimer);
            OnVerifyingTimerIsUp(new EventArgs());
            OnAllowingCursorToMove(new EventArgs());
            //Console.WriteLine("fin timer vérif");
            //if (!isFirstBlinkTimer)
            //{
            //    mmgt.setIsCursorAllowedToMove(true);//---------------------------------------------
            //}
        }

        /// <summary>
        /// Event called when first blink timer is finished, close timer, and allowMouseToMove
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnfirstBlinkTimerEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            CloseTimer(TimersAvailable.firstBlinkTimer);
            OnFirstTimerIsUp(new EventArgs());
            OnAllowingCursorToMove(new EventArgs());
            // mmgt.setIsCursorAllowedToMove(true);//---------------------------------------------

        }

    }
}


