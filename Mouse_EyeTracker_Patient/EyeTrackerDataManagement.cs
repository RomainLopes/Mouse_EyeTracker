using System;
using EyeXFramework;
using Tobii.EyeX.Framework;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace namespaceProgram
{

    /// <summary>
    /// Class containing all the event called when getting new eye-tracker data
    /// </summary>
    class EyeTrackerDataManagement
    {
        private int rawXPosition { get; set; } = 0;
        private int rawYPosition { get; set; } = 0;
        private int updateXPosition { get; set; } = 0;
        private int updateYPosition { get; set; } = 0;




        #region Cursor's position variables

        /// <summary>
        /// number of position used to calculate the average gaze's position
        /// </summary>
        private static int numberOfPositionRemembered = 25; // number of posittion for average position
        /// <summary>
        /// when clicking, it choose the "r-th" last position of the cursor
        /// </summary>
        private static int r = 10; //retard when clicking

        private static List<int> X_CursorAveragePosition_Remembered = new List<int>();
        private static List<int> Y_CursorAveragePosition_Remembered = new List<int>();
        private static List<int> X_CursorRawPosition_Remembered = new List<int>();
        private static List<int> Y_CursorRawPosition_Remembered = new List<int>();
        private static int X_Sum_CursorRawPosition = 0;
        private static int Y_Sum_CursorRawPosition = 0;
        #endregion

        /// <summary>
        /// Initialisation of the lists used for calculating the cursor's position
        /// </summary>
        public void InitialisationCursorPosition()
        {
            mmgt.isCursorAllowedToMove = true;
            // Console.WriteLine("InitialisationCursorPosition" + this.isCursorAllowedToMove);
            for (int i = 0; i < numberOfPositionRemembered; i++)
            {
                X_CursorRawPosition_Remembered.Add(0);
                Y_CursorRawPosition_Remembered.Add(0);
            }
            for (int i = 0; i < r; i++)
            {
                X_CursorAveragePosition_Remembered.Add(0);
                Y_CursorAveragePosition_Remembered.Add(0);
            }
        }

        /// <summary>
        ///retrun the new x or y position of the cursor, require the new position of the cursor
        /// and the classification of the position (x or y)
        /// </summary>
        /// <param name="position"> value of the new raw position acquired by the eye tracker</param>
        /// <param name="xy"> axis of the position </param>
        /// <remarks> it calculate the average position of the last n positions </remarks>
        /// <returns> the update position on the axis</returns>
        public int UpdateCursorPosition(int position, String xy)
        {
            switch (xy)
            {
                case "x":
                    X_Sum_CursorRawPosition += position;
                    X_Sum_CursorRawPosition -= X_CursorRawPosition_Remembered[0];
                    X_CursorRawPosition_Remembered.Add(position);
                    X_CursorRawPosition_Remembered.RemoveAt(0);
                    X_CursorAveragePosition_Remembered.Add(X_Sum_CursorRawPosition / numberOfPositionRemembered);
                    X_CursorAveragePosition_Remembered.RemoveAt(0);
                    return X_CursorAveragePosition_Remembered[r - 1];

                case "y":
                    Y_Sum_CursorRawPosition += position;
                    Y_Sum_CursorRawPosition -= Y_CursorRawPosition_Remembered[0];
                    Y_CursorRawPosition_Remembered.Add(position);
                    Y_CursorRawPosition_Remembered.RemoveAt(0);
                    Y_CursorAveragePosition_Remembered.Add(Y_Sum_CursorRawPosition / numberOfPositionRemembered);
                    Y_CursorAveragePosition_Remembered.RemoveAt(0);
                    return Y_CursorAveragePosition_Remembered[r - 1];
                default:
                    throw new ArgumentException("Unhandled position type: " + xy);
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

        /// <summary>
        /// get the new xy gaze position on the screen, update the cursor position and make it move using MouseAction's method
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e">GazePointEventArgs</param>
        public void OnGazeDataStreamEvent(Object source, GazePointEventArgs e)
        {
            rawXPosition = (int)Math.Truncate(e.X);
            rawYPosition = (int)Math.Truncate(e.Y);

            updateXPosition = UpdateCursorPosition(rawXPosition, "x");
            updateYPosition = UpdateCursorPosition(rawYPosition, "y");

            if(updateYPosition >= Screen.PrimaryScreen.Bounds.Height - 10 )
            {
                OnCursorInBottomMidlleScreen(new EventArgs());
            }

            mmgt.MoveMouse(updateXPosition, updateYPosition, mmgt.getIsCursorAllowedToMove());
        }

        /// <summary>
        /// getting the gaze's status of the user (looking or not looking at the screen)
        /// using it to detect blink and depending on wich timer is running
        /// freeze the cursor, click and/or start another timer
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e">EngineStateValue<GazeTracking></param>
        public void OnGazeTrackingChangedEvent(Object source, EngineStateValue<GazeTracking> e)
        {
            //Console.WriteLine("Gaze tracking (state-changed event): {0}", e);


            if (e.Value.ToString().Equals("GazeNotTracked"))
            {
                Console.Beep(200, 100);


                //if first eyes closing, freezing cursor and running blinkveryfing timer
                if (!tbm.isVerifyingBlinkTimer && !tbm.isFirstBlinkTimer)
                {
                    mmgt.setIsCursorAllowedToMove (false);
                    //Console.WriteLine("cursorDoesNotMove first blink");


                    tbm.StartTimer(TimerBlinkManagement.TimersAvailable.verifyingBlinkTimer);
                    //Console.WriteLine("fermeture sans rien, lance vérification clin d'oeil 1, curseur freeze");

                    if (StaticClass.isPanel1Activated && StaticClass.isPanel2Activated && StaticClass.isPanel3Activated && StaticClass.isPanel4Activated )
                    {
                        StaticClass.freezeCount += 1;
                    }
                    if (StaticClass.isPanel1Activated && StaticClass.isPanel2Activated && StaticClass.isPanel3Activated && StaticClass.isPanel4Activated
                        && StaticClass.isCursorOnPanel1 && !StaticClass.isReadyToClick && StaticClass.freezeCount > 1)
                    {
                        StaticClass.isReadyToClick = true;
                    }
                }

                //second eyes closing, run blinkverifying
                if (tbm.isFirstBlinkTimer)
                {
                    tbm.StartTimer(TimerBlinkManagement.TimersAvailable.verifyingBlinkTimer);
                    //Console.WriteLine("fermeture durant first timer, lance vérif, premier timer tourne ");
                }
            }
            else
            {

                if (e.Value.ToString().Equals("GazeTracked"))
                {
                    // Opening after first verification (firsttimer is false), 
                    if (tbm.isVerifyingBlinkTimer && !tbm.isFirstBlinkTimer)
                    {
                        tbm.CloseTimer(TimerBlinkManagement.TimersAvailable.verifyingBlinkTimer);
                        tbm.StartTimer(TimerBlinkManagement.TimersAvailable.firstBlinkTimer);

                        //Console.WriteLine("ouverture après vérif 1, lance firstTimer ");
                        //Console.WriteLine();
                    }

                    if (tbm.isVerifyingBlinkTimer && tbm.isFirstBlinkTimer)
                    {
                        //Console.WriteLine("début second timer");
                        tbm.CloseTimer(TimerBlinkManagement.TimersAvailable.verifyingBlinkTimer);
                        tbm.CloseTimer(TimerBlinkManagement.TimersAvailable.firstBlinkTimer);

                        leftEyeCounter = 0;
                        rightEyeCounter = 0;
                        tbm.StartTimer(TimerBlinkManagement.TimersAvailable.secondBlinkTimer);

                        // Console.WriteLine("ouverture durant first timer, lancement second timer");
                        //console.WriteLine();
                    }
                }
            }
        }
    }
}
