namespace EmployeeManager.Framework.Validation
{
    public class ModelPropertyChanged
    {
        public ModelPropertyChanged(IValidatableModel model, string propertyName)
        {
            Model = model;
            PropertyName = propertyName;
        }

        public IValidatableModel Model { get; private set; }
        public string PropertyName { get; private set; }
    }
}