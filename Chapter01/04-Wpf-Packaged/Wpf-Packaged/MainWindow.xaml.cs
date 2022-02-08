using System.Windows;
using Microsoft.Windows.System.Power;

namespace Wpf_Packaged
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtPowerStatus.Text = $"Power source: {PowerManager.PowerSourceKind}";
        }
    }
}
