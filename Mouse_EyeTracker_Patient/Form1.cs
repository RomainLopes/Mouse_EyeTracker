using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tobii.EyeX.Framework;
using EyeXFramework;



namespace Mouse_EyeTracker_Patient
{
    public partial class Form1 : Form
    {
        #region Variables
        public int maximunProgressBar { get; set; } = 2000;
        public void setMaximumProgressBar(int i)
        {
            this.maximunProgressBar = i;
        }

        private int freezeCount = 0;

        private Boolean isReadyToClick = false;

        private Boolean isCursorOnPanel1 = false;
        private Boolean isCursorOnPanel2 = false;
        private Boolean isCursorOnPanel3 = false;
        private Boolean isCursorOnPanel4 = false;

        public Boolean isPanel1Activated { get; set; } = false;
        public Boolean isPanel2Activated { get; set; } = false;
        public Boolean isPanel3Activated { get; set; } = false;
        public Boolean isPanel4Activated { get; set; } = false;

        private Boolean isLabelAVisible  = false;
        private Boolean isLabelBVisible  = false;
        private Boolean isLabelCVisible  = false;
        private Boolean isLabelDVisible  = false;

        public Boolean isCircularProgressBar1Complete { get; set; } = false;
        public Boolean isCircularProgressBar2Complete { get; set; } = false;
        public Boolean isCircularProgressBar3Complete { get; set; } = false;
        public Boolean isCircularProgressBar4Complete { get; set; } = false;

        private String firstMessage = "Keep the cursor inside each rectangle for at least 2s";
        private String secondMessage = "Now try to blink\n"
                                           + "(close your eyes and open them when you hear the \" beep \" sound)";
        private String thirdMessage = "Nice, you may have notice, the cursor had just freezed, \n"
                                          + "Now try to look at the biggest panel, then blink \n"
                                          + "(close your eyes and open them when you hear the \" beep \" sound)";
        private String forthMessage = "Real good, you know, blinking while the cursor is frozen does a left click \n" +
                                            "Go do a left click on each panel \n" +
                                            "(blink one time to freeze the cursor, \n" +
                                            "then blink a second time while the cursor is frozen to click)";
        private String fifthMessage = "You did it ! \n Now you can use this eye-tracked mouse ";
        #endregion

        #region events
        public EventHandler TriggerStartTimerCPB1;
        protected virtual void OnTriggerStartTimerCPB1(EventArgs e)
        {
            EventHandler handler = TriggerStartTimerCPB1;
            if (handler != null)
                handler(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTriggerStartTimerCPB1");
            }
        }

        public EventHandler TriggerStartTimerCPB2;
        protected virtual void OnTriggerStartTimerCPB2(EventArgs e)
        {
            if (TriggerStartTimerCPB2 != null)
                TriggerStartTimerCPB2(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTriggerStartTimerCPB2");
            }
        }

        public EventHandler TriggerStartTimerCPB3;
        protected virtual void OnTriggerStartTimerCPB3(EventArgs e)
        {
            if (TriggerStartTimerCPB3 != null)
                TriggerStartTimerCPB3(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTriggerStartTimerCPB3");
            }
        }

        public EventHandler TriggerStartTimerCPB4;
        protected virtual void OnTriggerStartTimerCPB4(EventArgs e)
        {
            if (TriggerStartTimerCPB4 != null)
                TriggerStartTimerCPB4(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTriggerStartTimerCPB4");
            }
        }

        public EventHandler TriggerCloseTimerCPB1;
        protected virtual void OnTriggerCloseTimerCPB1(EventArgs e)
        {
            if (TriggerCloseTimerCPB1 != null)
                TriggerCloseTimerCPB1(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTriggerCloseTimerCPB1");
            }
        }

        public EventHandler TriggerCloseTimerCPB2;
        protected virtual void OnTriggerCloseTimerCPB2(EventArgs e)
        {
            if (TriggerCloseTimerCPB2 != null)
                TriggerCloseTimerCPB2(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTriggerCloseTimerCPB2");
            }
        }

        public EventHandler TriggerCloseTimerCPB3;
        protected virtual void OnTriggerCloseTimerCPB3(EventArgs e)
        {
            if (TriggerCloseTimerCPB3 != null)
                TriggerCloseTimerCPB3(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTriggerCloseTimerCPB3");
            }
        }

        public EventHandler TriggerCloseTimerCPB4;
        protected virtual void OnTriggerCloseTimerCPB4(EventArgs e)
        {
            if (TriggerCloseTimerCPB4 != null)
                TriggerCloseTimerCPB4(this, e);
            else
            {
                Console.WriteLine("NullPointer OnTriggerCloseTimerCPB4");
            }
        }

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        #region setVisible form
        delegate void SetVisibleFormCallback(System.Windows.Forms.Form form, Boolean value);

        private void SetVisible(System.Windows.Forms.Form form, Boolean value)
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

        #region modify backColor panel
        delegate void modifyPanelColorCallback(System.Windows.Forms.Panel panel, System.Drawing.Color color);

        public void modifyPanelColor(System.Windows.Forms.Panel panel, System.Drawing.Color color)
        {
            if (panel.InvokeRequired)
            {
                modifyPanelColorCallback d = new modifyPanelColorCallback(modifyPanelColor);
                this.Invoke(d, new object[] { panel, color });
            }
            else
            {
                panel.BackColor = color;
            }
        }

        public void modifyPanel1BackColor(System.Drawing.Color color)
        {
            modifyPanelColor(panel1, System.Drawing.Color.Chartreuse);
        }

        public void modifyPanel2BackColor(System.Drawing.Color color)
        {
            modifyPanelColor(panel2, System.Drawing.Color.Chartreuse);
        }

        public void modifyPanel3BackColor(System.Drawing.Color color)
        {
            modifyPanelColor(panel3, System.Drawing.Color.Chartreuse);
        }

        public void modifyPanel4BackColor(System.Drawing.Color color)
        {
            modifyPanelColor(panel4, System.Drawing.Color.Chartreuse);
        }
        #endregion

        #region UpdateCircularProgressBar

        delegate void UpdateCircularProgressBarCallback(CircularProgressBar.CircularProgressBar cpb, int i);

        public void UpdateCircularProgressBar(CircularProgressBar.CircularProgressBar cpb, int i)
        {
            if (cpb.InvokeRequired)
            {
                UpdateCircularProgressBarCallback d = new UpdateCircularProgressBarCallback(UpdateCircularProgressBar);
                this.Invoke(d, new object[] { cpb, i });
            }
            else
            {
                cpb.Value = i;
            }
        }

        public void UpdateCircularProgressBar1(int i)
        {
            UpdateCircularProgressBar(circularProgressBar1, i);
        }

        public void UpdateCircularProgressBar2(int i)
        {
            UpdateCircularProgressBar(circularProgressBar2, i);
        }

        public void UpdateCircularProgressBar3(int i)
        {
            UpdateCircularProgressBar(circularProgressBar3, i);
        }

        public void UpdateCircularProgressBar4(int i)
        {
            UpdateCircularProgressBar(circularProgressBar4, i);
        }
        #endregion

        #region SetText Label
        delegate void SetTextLabelCallback(System.Windows.Forms.Label label, string text);

        private void SetLabelText(System.Windows.Forms.Label label, string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (label.InvokeRequired)
            {
                SetTextLabelCallback d = new SetTextLabelCallback(SetLabelText);
                this.Invoke(d, new object[] { label, text });
            }
            else
            {
                label.Text = text;
                label.Refresh();
            }
        }

        public void setLabel1Text(String instructionText)
        {
            SetLabelText(label1, instructionText);
        }
        #endregion

        #region SetVisiblePanel
        delegate void SetVisiblePanelCallback(System.Windows.Forms.Panel panel, Boolean value);

        private void SetVisible(System.Windows.Forms.Panel panel, Boolean value)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (panel.InvokeRequired)
            {
                SetVisiblePanelCallback d = new SetVisiblePanelCallback(SetVisible);
                this.Invoke(d, new object[] { panel, value });
            }
            else
            {
                panel.Visible = value;
                panel.Refresh();
            }
        }

        public void setPanel5Visible(Boolean value)
        {
            SetVisible(panel5, value);
        }

        public void setPanel6Visible(Boolean value)
        {
            SetVisible(panel6, value);
        }
        #endregion

       

        public void DisplayNextMessageLabelInstruction()
        {
            if (isPanel1Activated && isPanel2Activated && isPanel3Activated && isPanel4Activated &&
               isLabelAVisible && isLabelBVisible && isLabelCVisible && isLabelDVisible)
            {
                setLabel1Text(fifthMessage);
            }
            else
            if (isPanel1Activated && isPanel2Activated && isPanel3Activated && isPanel4Activated &&
                isReadyToClick)
            {
                setPanel5Visible(true);
                setLabel1Text(forthMessage);
            }
            else
            if (isPanel1Activated && isPanel2Activated && isPanel3Activated && isPanel4Activated &&
               freezeCount > 0)
            {
                setLabel1Text(thirdMessage);
            }
            else
            if (isPanel1Activated && isPanel2Activated && isPanel3Activated && isPanel4Activated &&
                freezeCount == 0)
            {
                setLabel1Text(secondMessage);
            }
            else
            {
                setLabel1Text(firstMessage);
            }

        }

        #region panel1 events
        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            isCursorOnPanel1 = true;
            if (isCircularProgressBar1Complete == false)
            {
                OnTriggerStartTimerCPB1(new EventArgs());
            }    
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            isCursorOnPanel1 = false;
            if (!isCircularProgressBar1Complete)
            {
                OnTriggerCloseTimerCPB1(new EventArgs());
            }
            DisplayNextMessageLabelInstruction();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (isPanel1Activated && isPanel2Activated && isPanel3Activated && isPanel4Activated)
            {
                setPanel5Visible(true);
                labelA.Visible = true;
                isLabelAVisible = true;
                DisplayNextMessageLabelInstruction();
            }
        }
        #endregion

        #region panel2 events
        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            isCursorOnPanel2 = true;
            if (!isCircularProgressBar2Complete)
            {
                OnTriggerStartTimerCPB2(new EventArgs());
            }
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            isCursorOnPanel2 = false;
            if (!isCircularProgressBar2Complete)
            {
                OnTriggerCloseTimerCPB2(new EventArgs());
            }
            DisplayNextMessageLabelInstruction();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            if (isPanel1Activated && isPanel2Activated && isPanel3Activated && isPanel4Activated)
            {
                setPanel5Visible(true);
                labelB.Visible = true;
                isLabelBVisible = true;
                DisplayNextMessageLabelInstruction();
            }
        }
        #endregion

        #region panel3 events
        private void panel3_MouseEnter(object sender, EventArgs e)
        {
            isCursorOnPanel3 = true;
            if (!isCircularProgressBar3Complete)
            {
                OnTriggerStartTimerCPB3(new EventArgs());
            }
        }

        private void panel3_MouseLeave(object sender, EventArgs e)
        {
            isCursorOnPanel3 = false;
            if (!isCircularProgressBar3Complete)
            {
                OnTriggerCloseTimerCPB3(new EventArgs());
            }
            DisplayNextMessageLabelInstruction();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            if (isPanel1Activated && isPanel2Activated && isPanel3Activated && isPanel4Activated)
            {
                setPanel5Visible(true);
                labelC.Visible = true;
                isLabelCVisible = true;
                DisplayNextMessageLabelInstruction();
            }
        }
        #endregion

        #region panel4 events
        private void panel4_MouseEnter(object sender, EventArgs e)
        {
            isCursorOnPanel4 = true;
            if (!isCircularProgressBar4Complete)
            {
                OnTriggerStartTimerCPB4(new EventArgs());
            }
        }

        private void panel4_MouseLeave(object sender, EventArgs e)
        {
            isCursorOnPanel4 = false;
            if (!isCircularProgressBar4Complete)
            {
                OnTriggerCloseTimerCPB4(new EventArgs());
            }
            DisplayNextMessageLabelInstruction();
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            if (isPanel1Activated && isPanel2Activated && isPanel3Activated && isPanel4Activated)
            {
                setPanel5Visible(true);
                labelD.Visible = true;
                isLabelDVisible = true;
                DisplayNextMessageLabelInstruction();
            }
        }
        #endregion

        #region loading and closing form event

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("set up cpb");

            DisplayNextMessageLabelInstruction();

            labelCloseEyesInstruction.Text = "Close your eyes before this message disappear \n (if you want to click) ";
            panel6.Visible = false;

            circularProgressBar1.Value = 0;
            circularProgressBar1.Minimum = 0;
            circularProgressBar1.Maximum = maximunProgressBar;


            circularProgressBar2.Value = 0;
            circularProgressBar2.Minimum = 0;
            circularProgressBar2.Maximum = maximunProgressBar;

            circularProgressBar3.Value = 0;
            circularProgressBar3.Minimum = 0;
            circularProgressBar3.Maximum = maximunProgressBar;

            circularProgressBar4.Value = 0;
            circularProgressBar4.Minimum = 0;
            circularProgressBar4.Maximum = maximunProgressBar;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }




        #endregion


    }
}

