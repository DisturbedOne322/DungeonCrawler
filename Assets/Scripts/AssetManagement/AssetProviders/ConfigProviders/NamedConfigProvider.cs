using AssetManagement.AssetProviders.Core;
using AssetManagement.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace AssetManagement.AssetProviders
{
    public class NamedConfigProvider<TConfigType> : BaseConfigProvider<TConfigType> where TConfigType : BaseConfig
    {
        private readonly string _configName;
        private TConfigType _loadedConfig;

        public NamedConfigProvider(IAssetLoader assetLoader, string configName)
            : base(assetLoader)
        {
            _configName = configName;
        }

        public override async UniTask Initialize()
        {
            _loadedConfig = await AssetLoader.Load<TConfigType>(_configName);
        }

        public override void Dispose()
        {
            if (_loadedConfig)
                Addressables.Release(_loadedConfig);
        }

        public TConfigType GetConfig()
        {
            return _loadedConfig;
        }
    }
}