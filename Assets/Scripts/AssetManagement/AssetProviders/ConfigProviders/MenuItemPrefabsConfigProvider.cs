using AssetManagement.AssetProviders.Core;
using AssetManagement.Configs;
using Constants;

namespace AssetManagement.AssetProviders.ConfigProviders
{
    public class MenuItemPrefabsConfigProvider : NamedConfigProvider<MenuItemsConfig>
    {
        public MenuItemPrefabsConfigProvider(IAssetLoader assetLoader) : base(assetLoader, ConstConfigs.MenuItemPrefabsConfig)
        {
        }
    }
}