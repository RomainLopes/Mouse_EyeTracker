using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mouse_EyeTracker_Patient
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            EyeTrackerDataManagement etdmgt = new EyeTrackerDataManagement();
            MouseManagement mmgt = new MouseManagement();
            TimerBlinkManagement tbmgt = new TimerBlinkManagement();

            EyeTracker_Management etmgt = new EyeTracker_Management(mmgt, tbmgt, etdmgt);


            Form1 f1 = new Form1();
            Form2 f2 = new Form2();
            TimerControl tc = new TimerControl();

            Form_Management fmgt = new Form_Management(tc, f1, f2, etmgt);

            fmgt.RunF1();

            Console.WriteLine("fermeture programme");
            etmgt.Close_EyeTracker_Management();
        }
    }
}
