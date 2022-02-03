using System.Threading.Tasks;

namespace UWP_Desktop.Contracts
{
    public interface IDialogService
    {
        Task<bool> ShowAsync(string title, string message, string yesButtonText, string noButtonText);
    }
}
