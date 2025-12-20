using AssetManagement.AssetProviders.Core;
using AssetManagement.Configs;
using Data.Constants;

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