using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkChat
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SignIn signIn = new SignIn();
            Application.Run(signIn);
            if (signIn.IsAuthenticated)
            {
                Application.Run(new Chat());
            }
        }
    }
}
