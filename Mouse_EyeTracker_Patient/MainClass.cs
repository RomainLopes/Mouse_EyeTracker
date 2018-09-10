using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mouse_EyeTracker_Patient
{
    static class MainClass
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


            FormDemo f1 = new FormDemo();
            FormCommands f2 = new FormCommands();
            TimerControl tc = new TimerControl();

            Form_Management fmgt = new Form_Management(tc, f1, f2, etmgt);

            
            Application.Run(fmgt);

            Console.WriteLine("fermeture programme");
            etmgt.Close_EyeTracker_Management();
        }
    }
}
