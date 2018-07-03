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
        #region setVisible form
        delegate void SetVisibleFormCallback(Form form, Boolean value);

        public void SetVisible(Form form, Boolean value)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (form.InvokeRequired)
            {
                SetVisibleFormCallback d = new SetVisibleFormCallback(SetVisible);
                this.Invoke(d, new object[] { form, value });
            }
            else
            {
                form.Visible = value;
                form.Refresh();
            }
        }
        #endregion

        public Form2()
        {
            InitializeComponent();
            

        }

        private Point calcultateCenterLocation(Panel panel, Label label)
        {
            return new Point((panel.Width - label.Width) / 2, (panel.Height - label.Height) / 2);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            label1.Location = calcultateCenterLocation(panel1, label1);
            label2.Location = calcultateCenterLocation(panel2, label2);
            label3.Location = calcultateCenterLocation(panel3, label3);
            label4.Location = calcultateCenterLocation(panel4, label4);
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
