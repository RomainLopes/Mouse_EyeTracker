using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mouse_EyeTracker_Patient
{
    class Form_Management
    {
        System.Drawing.Color ChartreuseColor = System.Drawing.Color.Chartreuse;

        private TimerControl tc;
        private Form1 f1;
        private Form2 f2;
        private EyeTracker_Management etmgt;

        public void OnTimerCPB1IsUp()
        {
            f1.modifyPanel1BackColor(ChartreuseColor);
            f1.isPanel1Activated = true;
            f1.isCircularProgressBar1Complete = true;
        }
        public void OnTimerCPB2IsUp()
        {
            f1.modifyPanel2BackColor(ChartreuseColor);
            f1.isPanel2Activated = true;
            f1.isCircularProgressBar2Complete = true;
        }
        public void OnTimerCPB3IsUp()
        {
            f1.modifyPanel3BackColor(ChartreuseColor);
            f1.isPanel3Activated = true;
            f1.isCircularProgressBar3Complete = true;
        }
        public void OnTimerCPB4IsUp()
        {
            f1.modifyPanel4BackColor(ChartreuseColor);
            f1.isPanel4Activated = true;
            f1.isCircularProgressBar4Complete = true;
        }

        public void OnCursorAllowedToMoveChanged(Boolean value)
        {
            if (f1.isPanel1Activated && f1.isPanel2Activated && f1.isPanel3Activated && f1.isPanel4Activated)
            {
                f1.freezeCount += 1;
            }
            if (f1.isPanel1Activated && f1.isPanel2Activated && f1.isPanel3Activated && f1.isPanel4Activated
                && f1.isCursorOnPanel1 && (f1.isReadyToClick == false) && f1.freezeCount > 1)
            {
                f1.isReadyToClick = true;
            }

            f1.DisplayNextMessageLabelInstruction();
            if (value == false && f1.freezeCount > 0)
            {
            f1.setPanel6Visible(true);
            //Cursor.Current = Cursors.Cross;
            }
            else
            {
           //Cursor.Current = Cursors.Default;
            f1.setPanel6Visible(false);
            }


        }

        public Form_Management(TimerControl tc, Form1 f1, Form2 f2, EyeTracker_Management etmgt)
        {
            this.tc = tc;
            this.f1 = f1;
            this.f2 = f2;
            this.etmgt = etmgt;

            //tc.timeMsTimer = 2000;
            //f1.maximunProgressBar = 2000;

            f1.TriggerStartTimerCPB1 += (s, e) => tc.timerCPB1Start();
            f1.TriggerStartTimerCPB2 += (s, e) => tc.timerCPB2Start();
            f1.TriggerStartTimerCPB3 += (s, e) => tc.timerCPB3Start();
            f1.TriggerStartTimerCPB4 += (s, e) => tc.timerCPB4Start();

            f1.TriggerCloseTimerCPB1 += (s, e) => tc.timerCPB1Close();
            f1.TriggerCloseTimerCPB2 += (s, e) => tc.timerCPB2Close();
            f1.TriggerCloseTimerCPB3 += (s, e) => tc.timerCPB3Close();
            f1.TriggerCloseTimerCPB4 += (s, e) => tc.timerCPB4Close();

            tc.TimerCPB1Update += (s, e) => f1.UpdateCircularProgressBar1(e.IntValue);
            tc.TimerCPB2Update += (s, e) => f1.UpdateCircularProgressBar2(e.IntValue);
            tc.TimerCPB3Update += (s, e) => f1.UpdateCircularProgressBar3(e.IntValue);
            tc.TimerCPB4Update += (s, e) => f1.UpdateCircularProgressBar4(e.IntValue);

            tc.TimerCPB1IsUp += (s, e) => this.OnTimerCPB1IsUp();
            tc.TimerCPB2IsUp += (s, e) => this.OnTimerCPB2IsUp();
            tc.TimerCPB3IsUp += (s, e) => this.OnTimerCPB3IsUp();
            tc.TimerCPB4IsUp += (s, e) => this.OnTimerCPB4IsUp();

            etmgt.CursorAllowedToMoveChanged += (s, e) => this.OnCursorAllowedToMoveChanged(e.BooleanValue);


        }

        public void RunF1()
        {
            Application.Run(f1);
        }



    }
}
