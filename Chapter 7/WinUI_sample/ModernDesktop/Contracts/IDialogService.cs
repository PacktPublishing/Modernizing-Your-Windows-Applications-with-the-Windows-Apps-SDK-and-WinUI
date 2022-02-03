using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;

namespace ModernDesktop.Contracts
{
    public interface IDialogService
    {
        Task<bool> ShowAsync(string title, string message, string yesButtonText, string noButtonText, XamlRoot xamlRoot);
    }
}
