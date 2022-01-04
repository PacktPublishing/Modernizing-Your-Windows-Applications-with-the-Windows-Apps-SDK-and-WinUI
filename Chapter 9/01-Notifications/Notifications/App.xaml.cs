using CommunityToolkit.WinUI.Notifications;
using Microsoft.UI.Xaml;
using System.Diagnostics;

namespace Notifications
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            ToastNotificationManagerCompat.OnActivated += eventArgs =>
            {
                ToastArguments args = ToastArguments.Parse(eventArgs.Argument);
                string action = args.Get("action");
                int messageId = args.GetInt("messageId");

                var input = eventArgs.UserInput;
                if (input.ContainsKey("reply"))
                {
                    string message = input["reply"].ToString();
                    Debug.WriteLine(message);
                }
                if (input.ContainsKey("response"))
                {
                    string response = input["response"].ToString();
                    Debug.WriteLine(response);
                }

                switch (action)
                {
                    case "click":
                        Debug.WriteLine("Click action");
                        break;
                    case "read":
                        Debug.WriteLine("Read action");
                        break;
                    case "flag":
                        Debug.WriteLine("Flag action");
                        break;
                }
            };

            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window m_window;
    }
}
