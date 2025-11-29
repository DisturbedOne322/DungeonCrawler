using AssetManagement.AssetProviders.Core;
using Constants;
using Gameplay.Configs;

namespace AssetManagement.AssetProviders.ConfigProviders
{
    public class GameplayConfigsProvider : LabeledConfigsProvider<GameplayConfig>
    {
        public GameplayConfigsProvider(IAssetLoader assetLoader) : base(assetLoader, ConstLabels.GameplayConfig)
        {
        }
    }
}