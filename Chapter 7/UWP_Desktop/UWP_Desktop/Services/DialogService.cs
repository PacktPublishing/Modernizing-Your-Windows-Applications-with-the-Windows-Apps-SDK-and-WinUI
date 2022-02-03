using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UWP_Desktop.Contracts;

namespace UWP_Desktop.Services
{
    public class DialogService : IDialogService
    {
        public async Task<bool> ShowAsync(string title, string message, string yesButtonText, string noButtonText)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                PrimaryButtonText = yesButtonText,
                CloseButtonText = noButtonText
            };
            var result = await dialog.ShowAsync();

            return result != ContentDialogResult.None;
        }
    }
}
