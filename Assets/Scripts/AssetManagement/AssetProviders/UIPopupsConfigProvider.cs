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

        public UIPopupsConfigProvider(IAssetLoader assetLoader) : base(assetLoader)
        {
        }

        public override T GetConfig<T>()
        {
            return _loadedConfig as T;
        }

        public override async UniTask Initialize()
        {
            _loadedConfig =
                await AssetLoader.Load<UIPopupsConfig>(ConstConfigs.UIPopupsConfig);
        }

        public override void Dispose()
        {
            Addressables.Release(_loadedConfig);
        }
    }
}