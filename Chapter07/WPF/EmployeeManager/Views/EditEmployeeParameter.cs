using EmployeeManager.Framework.Navigation;

namespace EmployeeManager.Views
{
    public class EditEmployeeParameter : INavigationParameter
    {
        private EditEmployeeParameter()
        {
        }

        public int EmployeeId { get; private set; }

        public EditAction EditAction { get; private set; }

        public static EditEmployeeParameter CreateAddEmployeeParameter()
        {
            return new EditEmployeeParameter
            {
                EditAction = EditAction.Add,
                EmployeeId = 0
            };
        }

        public static EditEmployeeParameter CreateEditEmployeeParameter(int employeeId)
        {
            return new EditEmployeeParameter
            {
                EditAction = EditAction.Edit,
                EmployeeId = employeeId
            };
        }
    }

    public enum EditAction
    {
        Add,
        Edit
    }
}