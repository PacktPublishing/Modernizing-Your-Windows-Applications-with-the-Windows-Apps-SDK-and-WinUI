using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ModernDesktop.Contracts;

namespace ModernDesktop.Services
{
    public class DialogService : IDialogService
    {
        public async Task<bool> ShowAsync(string title, string message, string yesButtonText, string noButtonText, XamlRoot xamlRoot)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                PrimaryButtonText = yesButtonText,
                CloseButtonText = noButtonText
            };
            dialog.XamlRoot = xamlRoot;
            var result = await dialog.ShowAsync();

            return result != ContentDialogResult.None;
        }
    }
}
