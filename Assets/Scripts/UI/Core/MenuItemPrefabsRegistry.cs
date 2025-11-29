using System;
using System.Collections.Generic;
using AssetManagement.AssetProviders;
using UI.BattleMenu;

namespace UI.Core
{
    public class MenuItemPrefabsRegistry
    {
        private readonly MenuItemPrefabsConfigProvider _menuItemPrefabsConfigProvider;

        private readonly Dictionary<Type, BaseMenuItemView> _popupsDict = new();
        
        private MenuItemPrefabsRegistry(MenuItemPrefabsConfigProvider menuItemPrefabsConfigProvider)
        {
            _menuItemPrefabsConfigProvider = menuItemPrefabsConfigProvider;
            BuildDictionary();
        }

        private void BuildDictionary()
        {
            var config = _menuItemPrefabsConfigProvider.GetConfig();
            
            foreach (var prefab in config.Prefabs)
                _popupsDict.Add(prefab.GetType(), prefab);
        }
        
        public T GetMenuItemPrefab<T>() where T : BaseMenuItemView
        {
            if (_popupsDict.TryGetValue(typeof(T), out var result))
                return result as T;
                
            throw new Exception($"Could not find prefab {typeof(T)}");
        }
    }
}