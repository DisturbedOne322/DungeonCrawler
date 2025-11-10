using AssetManagement.AssetProviders.Core;
using AssetManagement.Configs;
using Constants;

namespace AssetManagement.AssetProviders
{
    public class UIPopupsConfigProvider : NamedConfigProvider<UIPopupsConfig>
    {
        public UIPopupsConfigProvider(IAssetLoader assetLoader) : base(assetLoader, ConstConfigs.UIPopupsConfig)
        {
        }
    }
}