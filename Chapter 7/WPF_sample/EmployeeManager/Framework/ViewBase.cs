using System;
using System.Windows.Controls;

namespace EmployeeManager.Framework
{
    public class ViewBase : UserControl, IView
    {
        public object ViewModel
        {
            get { return DataContext; }
            set
            {
                if (!Object.ReferenceEquals(DataContext, value))
                {
                    DataContext = value;
                    OnViewModelChanged();
                }
            }
        }

        protected virtual void OnViewModelChanged()
        {
        }
    }
}