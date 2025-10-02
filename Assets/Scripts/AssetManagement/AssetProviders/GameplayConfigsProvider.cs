using System;
using System.Collections.Generic;
using AssetManagement.AssetProviders.Core;
using Constants;
using Cysharp.Threading.Tasks;
using Gameplay.Progression;
using UnityEngine.AddressableAssets;

namespace AssetManagement.AssetProviders
{
    public class GameplayConfigsProvider : BaseConfigProvider<GameplayConfig>
    {
        private readonly Dictionary<Type, GameplayConfig> _configsDict = new();
        
        public GameplayConfigsProvider(IAssetLoader assetLoader) : base(assetLoader) {}

        public override async UniTask Initialize()
        {
            List<string> labels = new()
            {
                ConstLabels.GameplayConfig,
            };
            
            var loadResult = await AssetLoader.LoadByLabel(labels);
            foreach (var config in loadResult)
            {
                if(config is not GameplayConfig)
                    throw new Exception($"Loaded config {config.GetType().Name} is not a GameplayConfig");
                
                _configsDict.Add(config.GetType(), config as GameplayConfig);
            }
        }

        public override T GetConfig<T>()
        {
            if(_configsDict.TryGetValue(typeof(T), out var config))
                return config as T;
            
            throw new Exception($"No GameplayConfig found for type {typeof(T).Name}");
        }

        public override void Dispose()
        {
            foreach (var config in _configsDict)
                Addressables.Release(config.Value);
            
            _configsDict.Clear();
        }
    }
}