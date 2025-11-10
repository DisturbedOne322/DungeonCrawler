using AssetManagement.Configs;
using Cysharp.Threading.Tasks;

namespace AssetManagement.AssetProviders.Core
{
    public abstract class BaseConfigProvider<TConfigType> : IAssetProvider where TConfigType : BaseConfig
    {
        protected IAssetLoader AssetLoader;

        public BaseConfigProvider(IAssetLoader assetLoader)
        {
            AssetLoader = assetLoader;
        }

        public abstract UniTask Initialize();
        public abstract void Dispose();
    }
}