using System;
using AssetManagement.Configs;
using Cysharp.Threading.Tasks;
using UI.Core;
using UnityEngine.AddressableAssets;

namespace AssetManagement.AssetProviders
{
    public class UIPrefabsProvider : IConfigProvider<BasePopup>
    {
        private UIPopupsConfig _config;

        public async UniTask Initialize() => _config = await LoadAsset<UIPopupsConfig>(ConstAddressableConfigNames.UIPopupsConfig);

        public async UniTask<T> LoadAsset<T>(string assetName) => await Addressables.LoadAssetAsync<T>(assetName);

        public T GetAsset<T>() where T : BasePopup
        {
            if (_config.TryGetPopup(out T popup))
                return popup;
            
            throw new Exception($"Prefab of type {typeof(T)} not found");
        }

        public void Dispose() => Addressables.Release(_config);
    }
}