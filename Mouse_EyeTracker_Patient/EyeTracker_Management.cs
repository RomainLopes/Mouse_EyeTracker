using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeXFramework;
using Tobii.EyeX.Framework;

namespace Mouse_EyeTracker_Patient
{
    class EyeTracker_Management
    {
        private EyeXHost eyeXHost;
        private GazePointDataStream lightlyFilteredGazeDataStream;

        private MouseManagement mmgt;
        public void setMouseManagement(MouseManagement mmgt)
        {
            this.mmgt = mmgt;
        }

        private TimerBlinkManagement tbm;
        public void setTimerBlinkManagement(TimerBlinkManagement tbm)
        {
            this.tbm = tbm;
        }

        private EyeTrackerDataManagement etdm;
        public void setEyeTrackerDataManagement(EyeTrackerDataManagement etdm)
        {
            this.etdm = etdm;
        }

        #region Event for the UI
        public delegate void CustomEventHandler(object sender, CustomEventArgs a);

        public CustomEventHandler CursorAllowedToMoveChanged;
        protected virtual void OnCursorAllowedToMoveChanged(CustomEventArgs e)
        {
            if (CursorAllowedToMoveChanged != null)
                CursorAllowedToMoveChanged(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTimerCPB1Update");
            }
        }
        #endregion

        public void OnGazeDataStream(object s, GazePointEventArgs e)
        {
            etdm.OnGazeDataStreamEvent(s, e);
            mmgt.MoveMouse(etdm.updateXPosition, etdm.updateYPosition, mmgt.getIsCursorAllowedToMove());
        }

        public void OnGazeNotTracked()
        {
            Console.Beep(200, 100);
            //if first eyes closing, freezing cursor and running blinkveryfing timer
            if ((tbm.getIsVerifyingBlinkTimer() == false) && (tbm.getIsFirstBlinkTimer() == false))
            {
                mmgt.setIsCursorAllowedToMove(false);
                tbm.StartTimer(TimerBlinkManagement.TimersAvailable.verifyingBlinkTimer);  
            }

            //second eyes closing, run blinkverifying, firstTimer still running
            if (tbm.getIsFirstBlinkTimer() == true)
            {
                tbm.StartTimer(TimerBlinkManagement.TimersAvailable.verifyingBlinkTimer);
            }
        }

        public void OnGazeTracked()
        {
            // Opening after first verification (firsttimer is false), start firstTimer
            if ((tbm.getIsVerifyingBlinkTimer() == true) && (tbm.getIsFirstBlinkTimer() == false))
            {
                tbm.CloseTimer(TimerBlinkManagement.TimersAvailable.verifyingBlinkTimer);
                tbm.StartTimer(TimerBlinkManagement.TimersAvailable.firstBlinkTimer);
            }

            //opening after second eyes closing
            if ((tbm.getIsVerifyingBlinkTimer() == true) && (tbm.getIsFirstBlinkTimer() == true))
            {
                tbm.CloseTimer(TimerBlinkManagement.TimersAvailable.verifyingBlinkTimer);
                tbm.CloseTimer(TimerBlinkManagement.TimersAvailable.firstBlinkTimer);

                mmgt.LeftClick();
                mmgt.setIsCursorAllowedToMove(true);

            }
        }

        public EventHandler CursorInBottomMidlleScreen;

        protected virtual void OnCursorInBottomMidlleScreen(EventArgs e)
        {
            if (CursorInBottomMidlleScreen != null)
                CursorInBottomMidlleScreen(this, e);
            else
            {
                Console.WriteLine("NullPointer OnCursorInBottomMidlleScreen");
            }
        }


        public EyeTracker_Management(MouseManagement mmgt,TimerBlinkManagement tbm , EyeTrackerDataManagement etdm)
        {
            this.tbm = tbm;
            this.mmgt = mmgt;
            this.etdm = etdm;

            eyeXHost = new EyeXHost();
            eyeXHost.Start();

            lightlyFilteredGazeDataStream = eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);
            lightlyFilteredGazeDataStream.Next += (s, e) => this.OnGazeDataStream(s, e);
            
            eyeXHost.GazeTrackingChanged += (s, e) => etdm.OnGazeTrackingChangedEvent(s, e);

            etdm.GazeNotTracked += (s, e) => this.OnGazeNotTracked();
            etdm.GazeTracked += (s, e) => this.OnGazeTracked();

            tbm.AllowingCursorToMove += (s, e) => mmgt.setIsCursorAllowedToMove(true);

            mmgt.CursorAllowedToMoveChanged += (s, e) => this.OnCursorAllowedToMoveChanged(new CustomEventArgs(e.BooleanValue));
            etdm.CursorInBottomMidlleScreen += (s, e) => this.OnCursorInBottomMidlleScreen(new EventArgs());
        }

        public void Close_EyeTracker_Management()
        {
            lightlyFilteredGazeDataStream.Dispose();
            eyeXHost.Dispose();
        }
    }
}
