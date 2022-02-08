using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UserControls
{
    public sealed partial class SearchBox : UserControl
    {
        public SearchBox()
        {
            this.InitializeComponent();
        }

        private void OnSearch(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Search started");
        }

        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(string), typeof(SearchBox), new PropertyMetadata(string.Empty, OnHeaderTextChanged));

        private static void OnHeaderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var searchBox = d as SearchBox;
            var header = searchBox.FindName("txtSearchHeader") as TextBlock;
            header.Text = e.NewValue.ToString();
        }


    }
}
