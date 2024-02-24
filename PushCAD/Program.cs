using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PushCAD
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string arg = "";
            try
            {
                foreach (string i in args)
                {
                    arg = arg + i + " ";
                }
                arg = arg.Remove(arg.Length - 1);
            }
            catch
            {
                //Nothing.
            }
            finally
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(arg));
            }
        }
    }
}
