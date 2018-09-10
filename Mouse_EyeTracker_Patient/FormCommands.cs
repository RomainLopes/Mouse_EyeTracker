using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace Mouse_EyeTracker_Patient
{

    public partial class FormCommands : Form
    {
        
        private Thread CursorPositionCheckThread;
        private System.Timers.Timer timerCheckPos;

        #region user32 import and methods
        [StructLayout(LayoutKind.Sequential)]
        public struct PointInter
        {
            public int X;
            public int Y;
            public static explicit operator Point(PointInter point) => new Point(point.X, point.Y);
        }
    
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out PointInter lpPoint);

        public static Point GetCursorPosition()
        {
            PointInter lpPoint;
            GetCursorPos(out lpPoint);
            return (Point)lpPoint;
        }
        #endregion

        public EventHandler ShowCommandPanel;

        protected virtual void OnShowCommandPanel(EventArgs e)
        {
            if (ShowCommandPanel != null)
                ShowCommandPanel(this, e);
            else
            {
                Console.WriteLine("NullPointer OnShowCommandPanel");
            }
        }

        public FormCommands()
        {
            InitializeComponent();
        }

        public void HideWhenLeave()
        {
            //getC
            this.Hide();
        }

        public new void Show()
        {
            if (!InvokeRequired)
                base.Show();
            else
                this.Invoke((MethodInvoker)(() => base.Show()));
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.DesktopLocation = new Point( (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - this.Height));
            double a = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            double b = (Screen.PrimaryScreen.Bounds.Height - this.Height);

            Thread CursorPositionCheckThread = new Thread(CursorPositionCheck);
            timerCheckPos = new System.Timers.Timer(10);
            timerCheckPos.Elapsed += (s, f) => MyControl_MouseLeave();
            CursorPositionCheckThread.Start();
        }

        public void CursorPositionCheck()
        {

        }


        //A chaque fois que le curseur sort d'un objet tu vérifie si il est encore dans le form avec la méthode en dessous et tu hide le form si ça sort


        private void MyControl_MouseLeave()
        {
            int a = this.Location.X + this.Width;
            int b = this.Location.Y + this.Height;

            Console.WriteLine("cursor pos: " + GetCursorPosition().X + " , " + GetCursorPosition().Y);
            Console.WriteLine("bound Xpos: " + this.Location.X + " and " + a);
            Console.WriteLine("bound Ypos: " + this.Location.Y + " and " + b);
            

            if (( GetCursorPosition().X <= this.Location.X ) || ( GetCursorPosition().X >= (this.Location.X + this.Width ) ) 
            || ( GetCursorPosition().Y <= this.Location.Y ) || ( GetCursorPosition().Y >= (this.Location.Y + this.Height ) ) )
            {
                Console.WriteLine("trigger");
                // the mouse is leaving the form
                this.Hide();
                timerCheckPos.Stop();
                Thread.Sleep(Timeout.Infinite);
            }
            else
            {
                // the mouse is still inside the form
            }
        }

       
    }
}
