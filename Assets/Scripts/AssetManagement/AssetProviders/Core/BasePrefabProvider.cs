using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AssetManagement.AssetProviders.Core
{
    public abstract class BasePrefabProvider : IAssetProvider
    {
        protected IAssetLoader AssetLoader;

        public BasePrefabProvider(IAssetLoader assetLoader)
        {
            AssetLoader = assetLoader;
        }

        public abstract UniTask Initialize();
        public abstract void Dispose();
        public abstract GameObject GetPrefab(string assetName);
    }
}