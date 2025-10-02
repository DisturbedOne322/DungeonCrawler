using System;
using System.Collections.Generic;
using AssetManagement.AssetProviders.Core;
using Constants;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AssetManagement.AssetProviders
{
    public class UIPrefabsProvider : BasePrefabProvider
    {
        private Dictionary<string, GameObject> _loadedPrefabs = new ();
        
        public UIPrefabsProvider(IAssetLoader assetLoader) : base(assetLoader)
        {
        }

        public override async UniTask Initialize()
        {
            List<string> labels = new()
            {
                ConstLabels.UIPrefab
            };
            
            _loadedPrefabs = await AssetLoader.LoadPrefabsByLabel(labels);
        }

        public override GameObject GetPrefab(string assetName)
        {
            if (_loadedPrefabs.TryGetValue(assetName, out GameObject prefab))
                return prefab;
            
            throw new KeyNotFoundException($"The asset {assetName} was not loaded.");
        }

        public override void Dispose()
        {
            foreach (var prefab in _loadedPrefabs.Values) 
                Addressables.Release(prefab);
            
            _loadedPrefabs.Clear();
        }
    }
}