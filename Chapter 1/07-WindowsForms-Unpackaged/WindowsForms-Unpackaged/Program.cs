using Microsoft.Windows.ApplicationModel.DynamicDependency;

namespace WindowsForms_Unpackaged
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Bootstrap.Initialize(0x00010000);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

            Bootstrap.Shutdown();
        }
    }
}