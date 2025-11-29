using System;
using System.Collections.Generic;
using AssetManagement.AssetProviders.Core;
using AssetManagement.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace AssetManagement.AssetProviders.ConfigProviders
{
    public class LabeledConfigsProvider<TConfigType> : BaseConfigProvider<TConfigType> where TConfigType : BaseConfig
    {
        private readonly Dictionary<Type, TConfigType> _configsDict = new();
        private readonly List<string> _labels;

        public LabeledConfigsProvider(IAssetLoader assetLoader, params string[] labels) : base(assetLoader)
        {
            _labels = new List<string>(labels);
        }

        public override async UniTask Initialize()
        {
            var loadResult = await AssetLoader.LoadByLabel(_labels);
            foreach (var config in loadResult)
            {
                if (config is not TConfigType)
                    throw new Exception($"Loaded config {config.GetType().Name} is not of type{typeof(TConfigType)}");

                _configsDict.Add(config.GetType(), config as TConfigType);
            }
        }

        public T GetConfig<T>() where T : TConfigType
        {
            if (_configsDict.TryGetValue(typeof(T), out var config))
                return config as T;

            throw new Exception($"No config found for type {typeof(T).Name}");
        }

        public override void Dispose()
        {
            foreach (var config in _configsDict)
                Addressables.Release(config.Value);

            _configsDict.Clear();
        }
    }
}