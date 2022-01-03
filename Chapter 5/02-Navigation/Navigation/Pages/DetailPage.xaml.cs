using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Navigation.Models;

namespace Navigation.Pages
{
    public sealed partial class DetailPage : Page
    {
        public DetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Person selectedPerson = e.Parameter as Person;
            txtName.Text = selectedPerson.Name;
            txtSurname.Text = selectedPerson.Surname;
            txtBirthdate.Text = selectedPerson.BirthDate.ToShortDateString();
        }
    }
}
