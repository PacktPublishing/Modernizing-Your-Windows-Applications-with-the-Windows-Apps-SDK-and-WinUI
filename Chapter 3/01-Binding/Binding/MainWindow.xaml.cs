using Binding.Models;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Binding
{

    public sealed partial class MainWindow : Window
    {
        public Person User { get; set; }

        public List<Person> People { get; set; }


        public MainWindow()
        {
            this.InitializeComponent();
            User = new Person
            {
                Name = "Matteo",
                Surname = "Pagani",
                BirthDate = new DateTime(1983, 9, 3)
            };

            People = new List<Person>
            {
                new Person { Name = "Matteo", Surname = "Pagani", BirthDate = new DateTime(1983, 9, 3), Budget = 50, Stocks = 5 },
                new Person { Name = "Marc", Surname = "Plogas", BirthDate = new DateTime(1980, 5, 2), Budget = 200, Stocks = 30 },
                new Person { Name = "John", Surname = "Doe", BirthDate = new DateTime(1990, 2, 9), Budget = 120, Stocks = 12 }
            };
        }

        public void DoSomething()
        {
            Debug.WriteLine("Do something");
        }
    }
}
