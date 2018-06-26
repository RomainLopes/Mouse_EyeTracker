using System;
//using namespaceProgram;

namespace namespaceProgram
{
    /// <summary>
    /// Timer's management methods, each timer get a public boolean that inform if the timer is running
    /// </summary>
    public class TimerBlinkManagement
    {
        #region timers instanciation
        private static System.Timers.Timer verifyingBlinkTimer;
        private static System.Timers.Timer firstBlinkTimer;
        private static System.Timers.Timer secondBlinkTimer;
        private MouseManagement mmgt;
        #endregion

        /// <summary>
        /// enumeration of the differents timer
        /// </summary>
        public enum TimersAvailable
        {
            verifyingBlinkTimer,
            firstBlinkTimer,
            secondBlinkTimer
        }


        public void setMouseManagement(MouseManagement mmgt)
        {
            this.mmgt = mmgt;
        }


        #region boolean data on is XXX timer's running
        public bool isVerifyingBlinkTimer { get; set; } = false;
        public bool isFirstBlinkTimer { get; set; } = false;
        public bool isSecondBlinkTimer { get; set; } = false;
        #endregion

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

            secondBlinkTimer = new System.Timers.Timer();
            secondBlinkTimer.Interval = 600;
            secondBlinkTimer.Elapsed += (sender, e) => OnSecondBlinkTimerEvent(sender, e, EyeTrackerDataManagement.updateXPosition, EyeTrackerDataManagement.updateYPosition);
            secondBlinkTimer.AutoReset = false;

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
                case TimersAvailable.secondBlinkTimer:
                    isSecondBlinkTimer = true;
                    secondBlinkTimer.Start();
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
                    mmgt.setIsCursorAllowedToMove(true); //---------------------------------------------
                    break;
                case TimersAvailable.firstBlinkTimer:
                    isFirstBlinkTimer = false;
                    firstBlinkTimer.Stop();
                    break;
                case TimersAvailable.secondBlinkTimer:
                    isSecondBlinkTimer = false;
                    secondBlinkTimer.Stop();
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
                case TimersAvailable.secondBlinkTimer:
                    isSecondBlinkTimer = false;
                    secondBlinkTimer.Close();
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

            //Console.WriteLine("fin timer vérif");
            if (!isFirstBlinkTimer)
            {
                mmgt.setIsCursorAllowedToMove(true);//---------------------------------------------
            }
        }

        /// <summary>
        /// Event called when first blink timer is finished, close timer, and allowMouseToMove
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnfirstBlinkTimerEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            mmgt.setIsCursorAllowedToMove(true);//---------------------------------------------

            CloseTimer(TimersAvailable.firstBlinkTimer);
        }

        /// <summary>
        /// Event called when second blink timer is finished, close timer, allowMouseToMove and
        /// left or right click depending on the blinking during the second timer's duration
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnSecondBlinkTimerEvent(Object source, System.Timers.ElapsedEventArgs e, int leftEyeCounter, int rightEyeCounter, int updateXPosition, int updateYPosition)
        {

            mmgt.LeftClick();//---------------------------------------------


            mmgt.setIsCursorAllowedToMove(true);//---------------------------------------------
            //Console.WriteLine("cursorMoves event secondblink");


            CloseTimer(TimersAvailable.secondBlinkTimer);
            //Console.WriteLine("fin second timer, cursor moves");
            //Console.WriteLine();
        }
    }
}


