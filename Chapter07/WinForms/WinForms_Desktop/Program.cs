using System;
using System.Windows.Forms;
using WinForms_Desktop.Data;

namespace WinForms_Desktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //making sure the database is available when starting the application for the first time
            using (var context = new EmployeeContext())
            {
                if (!context.Database.Exists())
                {
                    context.Database.Initialize(true);
                    Console.WriteLine("seeding database");
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
