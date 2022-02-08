using DataBinding.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DataBinding
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Person person;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            person = new Person
            {
                Name = "Matteo",
                Surname = "Pagani",
                Age = 38
            };
            MyPanel.DataContext = person;

            List<Person> people = new List<Person>
            {
                new Person { Name = "Matteo", Surname = "Pagani", Age = 38},
                new Person { Name = "Marc", Surname = "Plogas", Age = 27},
                new Person { Name = "John", Surname = "Doe", Age = 16}
            };

            lstPeople.ItemsSource = people;
            lstPeopleWithSelector.ItemsSource = people;
        }

        private void OnChangeName(object sender, RoutedEventArgs e)
        {
            person.Name = "Marc";
        }
    }
}
