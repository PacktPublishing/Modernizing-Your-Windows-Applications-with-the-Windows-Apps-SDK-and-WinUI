using Microsoft.Windows.ApplicationModel.Resources;

namespace Localization
{
    public sealed class AppResourceManager
    {
        private static AppResourceManager instance = null;
        private static ResourceManager _resourceManager = null;
        private static ResourceContext _resourceContext = null;

        public static AppResourceManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new AppResourceManager();
                return instance;
            }
        }

        private AppResourceManager()
        {
            _resourceManager = new ResourceManager();
            _resourceContext = _resourceManager.CreateResourceContext();
        }

        public string GetString(string name)
        {
            var result = _resourceManager.MainResourceMap.GetValue($"Resources/{name.Replace(".", "/")}", _resourceContext).ValueAsString;
            return result;
        }
    }

}
