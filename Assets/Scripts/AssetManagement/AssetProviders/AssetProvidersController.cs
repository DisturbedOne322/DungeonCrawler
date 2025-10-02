using System.Collections.Generic;
using AssetManagement.AssetProviders.Core;
using Cysharp.Threading.Tasks;

namespace AssetManagement.AssetProviders
{
    public class AssetProvidersController
    {
        private readonly List<IAssetProvider> _assetProviders = new ();

        public AssetProvidersController(List<IAssetProvider> assetProviders)
        {
            foreach (var assetProvider in assetProviders) 
                _assetProviders.Add(assetProvider);
        }

        public async UniTask Initialize()
        {
            UniTask[] initializeTasks = new UniTask[_assetProviders.Count];

            for (int i = 0; i < _assetProviders.Count; i++) 
                initializeTasks[i] = _assetProviders[i].Initialize();
            
            await UniTask.WhenAll(initializeTasks);
        }

        public void Dispose()
        {
            foreach (var assetProvider in _assetProviders) 
                assetProvider.Dispose();
        }
    }
}