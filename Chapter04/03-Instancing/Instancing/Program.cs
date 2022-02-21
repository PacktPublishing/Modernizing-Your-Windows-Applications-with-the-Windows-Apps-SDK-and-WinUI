using Microsoft.UI.Dispatching;
using Microsoft.Windows.AppLifecycle;
using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Storage;

namespace Instancing
{
    public static class Program
    {
        [STAThread]
        static async Task<int> Main(string[] args)
        {
            WinRT.ComWrappersSupport.InitializeComWrappers();

            bool isRedirect = await DecideRedirection();

            if (!isRedirect)
            {
                Microsoft.UI.Xaml.Application.Start((p) =>
                {
                    var context = new DispatcherQueueSynchronizationContext(
                        DispatcherQueue.GetForCurrentThread());
                    SynchronizationContext.SetSynchronizationContext(context);
                    new App();
                });
            }

            return 0;
        }

        private static async Task<bool> DecideRedirection()
        {
            bool isRedirect = false;
            // Find out what kind of activation this is.
            AppActivationArguments args = AppInstance.GetCurrent().GetActivatedEventArgs();
            ExtendedActivationKind kind = args.Kind;
            if (kind == ExtendedActivationKind.File)
            {
                // This is a file activation: here we'll get the file information,
                // and register the file name as our instance key.
                if (args.Data is IFileActivatedEventArgs fileArgs)
                {
                    IStorageItem file = fileArgs.Files[0];
                    AppInstance keyInstance = AppInstance.FindOrRegisterForKey(file.Name);

                    // If we successfully registered the file name, we must be the
                    // only instance running that was activated for this file.
                    if (keyInstance != null && !keyInstance.IsCurrent)
                    {
                        isRedirect = true;
                        await keyInstance.RedirectActivationToAsync(args);
                    }
                }
            }

            return isRedirect;
        }

    }

}
