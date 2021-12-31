using Microsoft.Windows.System.Power;
using System.Windows;

namespace Wpf_Unpackaged
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
