using AssetManagement.AssetProviders.Core;
using AssetManagement.Configs;
using Constants;

namespace AssetManagement.AssetProviders.ConfigProviders
{
    public class UIPopupsConfigProvider : NamedConfigProvider<UIPopupsConfig>
    {
        public UIPopupsConfigProvider(IAssetLoader assetLoader) : base(assetLoader, ConstConfigs.UIPopupsConfig)
        {
        }
    }
}