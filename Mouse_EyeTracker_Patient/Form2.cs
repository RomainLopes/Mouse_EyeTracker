using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Mouse_EyeTracker_Patient
{
    /// <summary>
    /// Struct representing a point.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public static implicit operator Point(POINT point)
        {
            return new Point(point.X, point.Y);
        }
    }

    /// <summary>
    /// Retrieves the cursor's position, in screen coordinates.
    /// </summary>
    /// <see>See MSDN documentation for further information.</see>
    [DllImport("user32.dll")]
    static extern bool GetCursorPos(out POINT lpPoint);

    public static Point GetCursorPosition()
    {
        POINT lpPoint;
        GetCursorPos(out lpPoint);
        //bool success = User32.GetCursorPos(out lpPoint);
        // if (!success)

        return lpPoint;
    }





    public partial class Form2 : Form
    {
        public EventHandler CursorMoves;
        protected virtual void OnCursorMoves(EventArgs e)
        {
            if (CursorMoves != null)
                CursorMoves(this, e);
            else
            {
                Console.WriteLine("NullPointer OnCursorMoves");
            }
        }

        public Form2()
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

            
        }


        //A chaque fois que le curseur sort d'un objet tu vérifie si il est encore dans le form avec la méthode en dessous et tu hide le form si ça sort


        private void MyControl_MouseLeave()
        {
            int a = this.Location.X + this.Width;
            int b = this.Location.Y + this.Height;

            Console.WriteLine("cursor pos: " + Cursor.Position.X + " , " + Cursor.Position.Y);
            Console.WriteLine("bound Xpos: " + this.Location.X + " and " + a);
            Console.WriteLine("bound Ypos: " + this.Location.Y + " and " + b);
            

            if ((Cursor.Position.X <= this.Location.X + 8) || (Cursor.Position.X >= (this.Location.X + this.Width-8) ) 
            || (Cursor.Position.Y <= this.Location.Y + 20) || (Cursor.Position.Y >= (this.Location.Y + this.Height-8) ) )
            {
                Console.WriteLine("trigger");
                // the mouse is leaving the form
                this.Hide();
            }
            else
            {
                // the mouse is still inside the form
            }
        }

        private void Form2_MouseLeave(object sender, EventArgs e)
        {
            MyControl_MouseLeave();
        }

        private void splitContainer1_MouseLeave(object sender, EventArgs e)
        {
            MyControl_MouseLeave();
        }

        private void splitContainer1_Panel1_MouseLeave(object sender, EventArgs e)
        {
            MyControl_MouseLeave();
        }

        private void panel5_MouseLeave(object sender, EventArgs e)
        {
            MyControl_MouseLeave();
        }

        private void splitContainer1_Panel2_MouseLeave(object sender, EventArgs e)
        {
            MyControl_MouseLeave();
        }

        private void tableLayoutPanel1_MouseLeave(object sender, EventArgs e)
        {
            MyControl_MouseLeave();
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            MyControl_MouseLeave();
        }

        private void panel3_MouseLeave(object sender, EventArgs e)
        {
            MyControl_MouseLeave();
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            MyControl_MouseLeave();
        }
    }
}
