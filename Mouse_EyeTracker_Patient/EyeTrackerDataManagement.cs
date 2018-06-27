using System;
using EyeXFramework;
using Tobii.EyeX.Framework;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Mouse_EyeTracker_Patient
{

    /// <summary>
    /// Class containing all the event called when getting new eye-tracker data
    /// </summary>
    class EyeTrackerDataManagement
    {
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
        private int rawXPosition { get; set; } = 0;
        private int rawYPosition { get; set; } = 0;
        public int updateXPosition { get; set; } = 0;
        public int updateYPosition { get; set; } = 0;
        private static int X_Sum_CursorRawPosition = 0;
        private static int Y_Sum_CursorRawPosition = 0;
        #endregion

        /// <summary>
        /// Initialisation of the lists used for calculating the cursor's position
        /// </summary>
        public EyeTrackerDataManagement()
        {
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



        #region GazeDataStream Event
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

            if (updateYPosition >= Screen.PrimaryScreen.Bounds.Height - 10)
            {
                OnCursorInBottomMidlleScreen(new EventArgs());
            }
           
        }
        #endregion

        #region GazeTracking Event
        public EventHandler GazeTracked;
        public EventHandler GazeNotTracked;

        protected virtual void OnGazeTracked(EventArgs e)
        {
            if (GazeTracked != null)
                GazeTracked(this, e);
            else
            {
                Console.WriteLine("NullPointer OnGazeTracked");
            }
        }
        protected virtual void OnGazeNotTracked(EventArgs e)
        {
            if (GazeNotTracked != null)
                GazeNotTracked(this, e);
            else
            {
                Console.WriteLine("NullPointer OnGazeNotTracked");
            }
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
                OnGazeNotTracked(new EventArgs());
            }

            if (e.Value.ToString().Equals("GazeTracked"))
            {
                OnGazeTracked(new EventArgs());
            }
        }
        #endregion
    }
}
