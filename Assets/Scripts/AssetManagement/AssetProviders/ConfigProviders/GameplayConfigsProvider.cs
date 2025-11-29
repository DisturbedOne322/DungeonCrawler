using AssetManagement.AssetProviders.Core;
using Constants;
using Gameplay.Configs;

namespace AssetManagement.AssetProviders
{
    public class GameplayConfigsProvider : LabeledConfigsProvider<GameplayConfig>
    {
        public GameplayConfigsProvider(IAssetLoader assetLoader) : base(assetLoader, ConstLabels.GameplayConfig)
        {
        }
    }
}