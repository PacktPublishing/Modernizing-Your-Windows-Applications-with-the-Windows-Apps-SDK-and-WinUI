using Microsoft.UI;
using Microsoft.UI.Xaml.Media;

namespace Binding.Helpers
{
    public static class BudgetColorHandler
    {
        public static SolidColorBrush GetBudgetColor(double budget, double stocks)
        {
            if (budget < 100 || stocks < 10)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else
            {
                return new SolidColorBrush(Colors.Green);
            }
        }
    }
}
