using AssetManagement.AssetProviders.Core;
using AssetManagement.Configs;
using Constants;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace AssetManagement.AssetProviders
{
    public class UIPopupsConfigProvider : BaseConfigProvider<UIPopupsConfig>
    {
        private UIPopupsConfig _loadedConfig;
        
        public UIPopupsConfigProvider(IAssetLoader assetLoader) : base(assetLoader) {}

        public override async UniTask Initialize()
        {
            _loadedConfig =
                await AssetLoader.Load<UIPopupsConfig>(ConstConfigs.UIPopupsConfig);
        }

        public override UIPopupsConfig GetConfig() => _loadedConfig;

        public override void Dispose() => Addressables.Release(_loadedConfig);
    }
}