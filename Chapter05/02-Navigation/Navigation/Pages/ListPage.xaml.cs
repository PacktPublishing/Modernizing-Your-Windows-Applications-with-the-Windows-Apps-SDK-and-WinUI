using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Navigation.Models;
using System;
using System.Collections.Generic;

namespace Navigation.Pages
{
    public sealed partial class ListPage : Page
    {
        public ListPage()
        {
            this.InitializeComponent();
            var people = new List<Person>
            {
                new Person { Name = "Matteo", Surname = "Pagani", BirthDate = new DateTime(1983, 9, 3), Budget = 50, Stocks = 5 },
                new Person { Name = "Marc", Surname = "Plogas", BirthDate = new DateTime(1980, 5, 2), Budget = 200, Stocks = 30 },
                new Person { Name = "John", Surname = "Doe", BirthDate = new DateTime(1990, 2, 9), Budget = 120, Stocks = 12 }
            };

            lstPeople.ItemsSource = people;
        }

        private void lstPeople_ItemClick(object sender, ItemClickEventArgs e)
        {
Person selectedPerson = e.ClickedItem as Person;
App.ShellFrame.Navigate(typeof(DetailPage), selectedPerson, new DrillInNavigationTransitionInfo());
        }
    }
}
