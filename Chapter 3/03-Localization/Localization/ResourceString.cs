using Microsoft.UI.Xaml.Markup;

namespace Localization
{
    [MarkupExtensionReturnType(ReturnType = typeof(string))]
    public sealed class ResourceString : MarkupExtension
    {

        public string Name
        {
            get; set;
        }

        protected override object ProvideValue()
        {
            string value = AppResourceManager.Instance.GetString(Name);
            return value;
        }
    }

}
