using AssetManagement.AssetProviders.Core;
using AssetManagement.Configs;
using Constants;

namespace AssetManagement.AssetProviders.ConfigProviders
{
    public class DungeonVisualsConfigProvider : NamedConfigProvider<DungeonVisualsConfig>
    {
        public DungeonVisualsConfigProvider(IAssetLoader assetLoader) : base(assetLoader,
            ConstConfigs.DungeonVisualsConfig)
        {
        }
    }
}