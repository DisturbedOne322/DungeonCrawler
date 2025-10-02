using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace AssetManagement.AssetProviders.Core
{
    public interface IAssetLoader
    {
        public UniTask Initialize();
        public UniTask<List<Object>> LoadByLabel(List<string> labels,
            Addressables.MergeMode mergeMode = Addressables.MergeMode.Union);

        public UniTask<Dictionary<string, GameObject>> LoadPrefabsByLabel(List<string> labels,
            Addressables.MergeMode mergeMode = Addressables.MergeMode.Union);

        public UniTask<T> Load<T>(string address) where T : class;
    }
}