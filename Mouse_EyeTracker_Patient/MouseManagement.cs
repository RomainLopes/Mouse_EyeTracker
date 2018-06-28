using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace Mouse_EyeTracker_Patient
{

    /// <summary>
    /// Classe contenant les variables et import user32.dll nécessaire aux méthodes de gestion
    /// des actions de la souris et de la position du curseur
    /// </summary>
    public class MouseManagement
    {

        private Boolean isCursorAllowedToMove = true;

        public EventHandler CursorAllowedToMove;
        public EventHandler CursorNotAllowedToMove;

        protected virtual void OnCursorAllowedToMove(EventArgs e)
        {
            if (CursorAllowedToMove != null)
                CursorAllowedToMove(this, e);
            else
            {
                Console.WriteLine("NullPointer OnCursorAllowedToMove");
            }
        }

        protected virtual void OnCursorNotAllowedToMove(EventArgs e)
        {
            if (CursorNotAllowedToMove != null)
                CursorNotAllowedToMove(this, e);
            else
            {
                Console.WriteLine("NullPointer OnNCursorNotAllowedToMove");
            }
        }

        public bool getIsCursorAllowedToMove()
        {
            return isCursorAllowedToMove;
        }

        public void setIsCursorAllowedToMove(Boolean value)
        {
            isCursorAllowedToMove = value;

            if (value == true)
            {
                OnCursorAllowedToMove(new EventArgs());
            }
            else
            {
                OnCursorNotAllowedToMove(new EventArgs());
            }

            // frn.DisplayNextMessageLabelInstruction();----------------------------
            // if (value == false && StaticClass.freezeCount > 0)
            // {
            //frn.setPanel6Visible(true);
            //Cursor.Current = Cursors.Cross;
            // }
            // else
            // {
            //Cursor.Current = Cursors.Default;
            //frn.setPanel6Visible(false);
            //  }
        }

        #region Windows API (user32.dll) imported Code

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        static extern void mouse_event(int flags, int dX, int dY, int buttons, int extraInfo);

        const int MOUSEEVENTF_MOVE = 0x1;
        const int MOUSEEVENTF_LEFTDOWN = 0x2;
        const int MOUSEEVENTF_LEFTUP = 0x4;
        const int MOUSEEVENTF_RIGHTDOWN = 0x8;
        const int MOUSEEVENTF_RIGHTUP = 0x10;
        const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
        const int MOUSEEVENTF_MIDDLEUP = 0x40;
        const int MOUSEEVENTF_WHEEL = 0x800;
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        #endregion

        /// <summary>
        /// move the cursor to the xy position if isCursorAllowedToMove is true
        /// </summary>
        /// <param name="x"> value of cursor position on x axis</param>
        /// <param name="y"> value of cursor position on y axis</param>
        /// <param name="isCursorAllowedToMove"> if false, the cursor won't move </param>
        public void MoveMouse(int x, int y, Boolean isCursorAllowedToMove)
        {
            //Console.WriteLine(isCursorAllowedToMove);

            if (isCursorAllowedToMove)
            {
                SetCursorPos(x, y);
            }
        }

        /// <summary>
        /// Do a left click to the cursor's position
        /// </summary>
        public void LeftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        /// <summary>
        /// Do a right click to the cursor's position
        /// </summary>
        public void RightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        public void MouseLeftDown()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }

        public void MouseLeftUp()
        {
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

    }
}
