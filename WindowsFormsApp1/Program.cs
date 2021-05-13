using System;
using System.Windows.Forms;

namespace DeskDeviceInfo
{
    static class Program
    {
        static System.Threading.Mutex mutex = new System.Threading.Mutex(true, "{LNSDF9U3-B9A1-45fd-ND9W-72F04E6BDE8F}");
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                mutex.ReleaseMutex();
            }
            else
            {
                //MessageBox.Show("only one instance at a time");
            }
        }

        ///// <summary>
        ///// 应用程序的主入口点。
        ///// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new Form1());
        //}
    }
}
