using AssetManagement.AssetProviders.Core;
using AssetManagement.Configs;
using Data.Constants;

namespace AssetManagement.AssetProviders.ConfigProviders
{
    public class MenuItemPrefabsConfigProvider : NamedConfigProvider<MenuItemsConfig>
    {
        public MenuItemPrefabsConfigProvider(IAssetLoader assetLoader) : base(assetLoader, ConstConfigs.MenuItemPrefabsConfig)
        {
        }
    }
}