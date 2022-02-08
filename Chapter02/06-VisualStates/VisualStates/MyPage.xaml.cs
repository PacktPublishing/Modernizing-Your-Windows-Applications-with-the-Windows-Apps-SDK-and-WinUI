using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
namespace VisualStates
{
    public sealed partial class MyPage : Page
    {
        public MyPage()
        {
            this.InitializeComponent();
        }

        private void MyButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "HiddenState", false);
        }

        private void MyButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "NormalState", false);
        }
    }
}
