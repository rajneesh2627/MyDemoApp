using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PL.Student
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (GetIp() == "192.168.0.184")
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new StudentForm());
            }
            else
            {
                MessageBox.Show("You aren't authorized to access the app", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Gets the IP of the machine.
        /// </summary>
        /// <returns></returns>
        static string GetIp()
        {
            IPAddress localIPs = Dns.GetHostAddresses(Dns.GetHostName()).ToList().Where(x => x.AddressFamily.ToString() == "InterNetwork").FirstOrDefault();
            return localIPs.ToString();
        }
    }
}
