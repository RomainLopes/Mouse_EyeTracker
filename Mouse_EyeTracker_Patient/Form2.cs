using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mouse_EyeTracker_Patient
{
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
            Console.WriteLine( a +" , " + b  );
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Hide();
        }

        private void Form2_Activated(object sender, EventArgs e)
        {
           // this.Hide();
        }

        //A chaque fois que le curseur sort d'un objet tu vérifie si il est encore dans le form avec la méthode en dessous et tu hide le form si ça sort

        private void MyControl_MouseLeave(object sender, EventArgs e)
        {
            if ((Cursor.Position.X <= this.Location.X) || (Cursor.Position.X >= (this.Location.X + this.Width) ) 
            || (Cursor.Position.Y <= this.Location.Y) || (Cursor.Position.Y >= (this.Location.Y + this.Height) ) )
            {
                this.Hide();
            }
            else
            {
                // the mouse is still inside the form
            }
        }

        private void Form2_MouseLeave(object sender, EventArgs e)
        {

        }

        private void splitContainer1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void panel5_MouseLeave(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_MouseLeave(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void panel3_MouseLeave(object sender, EventArgs e)
        {

        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {

        }
    }
}
