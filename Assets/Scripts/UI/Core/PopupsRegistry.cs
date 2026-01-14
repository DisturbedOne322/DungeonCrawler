using System;
using System.Collections.Generic;
using AssetManagement.AssetProviders.ConfigProviders;

namespace UI.Core
{
    public class PopupsRegistry
    {
        private readonly Dictionary<Type, BasePopup> _popupsDict = new();
        private readonly UIPopupsConfigProvider _uiPopupsConfigProvider;

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