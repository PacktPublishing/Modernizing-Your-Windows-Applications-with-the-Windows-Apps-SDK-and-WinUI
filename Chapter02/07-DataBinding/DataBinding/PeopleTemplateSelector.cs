using DataBinding.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace DataBinding
{
    public class PeopleTemplateSelector : DataTemplateSelector
    {
        public DataTemplate YoungTemplate { get; set; }

        public DataTemplate AdultTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            Person person = item as Person;
            if (person.Age < 18)
            {
                return YoungTemplate;
            }
            else
            {
                return AdultTemplate;
            }
        }
    }

}
