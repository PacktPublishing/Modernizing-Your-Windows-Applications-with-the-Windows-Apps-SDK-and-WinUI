using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.Generic;

namespace RelativePanel
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            List<Customer> customers = new()
            {
                new Customer("Matteo", "Pagani", 38),
                new Customer("Marc", "Plogas", 27),
                new Customer("John", "Doe", 15)
            };

            dgCustomers.ItemsSource = customers;

            List<Order> orders = new()
            {
                new Order("Milk", 5, 28),
                new Order("Coffee", 3, 49),
                new Order("Meat", 2, 56)
            };

            dgOrders.ItemsSource = orders;
        }
    }

    public record Customer(string Name, string Surname, int Age);

    public record Order (string Product, int Total, double Price);
}
