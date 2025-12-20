using AssetManagement.AssetProviders.Core;
using AssetManagement.Configs;
using Data.Constants;

namespace AssetManagement.AssetProviders.ConfigProviders
{
    public class UIPopupsConfigProvider : NamedConfigProvider<UIPopupsConfig>
    {
        public UIPopupsConfigProvider(IAssetLoader assetLoader) : base(assetLoader, ConstConfigs.UIPopupsConfig)
        {
        }
    }
}