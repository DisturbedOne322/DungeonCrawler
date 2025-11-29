using System;
using System.Collections.Generic;
using AssetManagement.AssetProviders;
using UI.Core;

namespace UI
{
    public class PopupsRegistry
    {
        private readonly UIPopupsConfigProvider _uiPopupsConfigProvider;

        private readonly Dictionary<Type, BasePopup> _popupsDict = new();
        
        private PopupsRegistry(UIPopupsConfigProvider uiPopupsConfigProvider)
        {
            _uiPopupsConfigProvider = uiPopupsConfigProvider;
            BuildDictionary();
        }

        private void BuildDictionary()
        {
            var config = _uiPopupsConfigProvider.GetConfig();
            
            foreach (var prefab in config.Prefabs)
                _popupsDict.Add(prefab.GetType(), prefab);
        }
        
        public T GetPopupPrefab<T>() where T : BasePopup
        {
            if (_popupsDict.TryGetValue(typeof(T), out var result))
                return result as T;
                
            throw new Exception($"Could not find popup {typeof(T)}");
        }
    }
}