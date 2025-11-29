using System;
using System.Collections.Generic;
using UI.BattleMenu;
using UnityEngine;

namespace AssetManagement.Configs
{
    [CreateAssetMenu(fileName = "UIMenuItemsConfig", menuName = "Visuals/UI/UIMenuItemsConfig")]
    public class MenuItemsConfig : BaseConfig
    {
        [SerializeField] private List<BaseMenuItemView> _prefabs;
        
        public IReadOnlyList<BaseMenuItemView> Prefabs => _prefabs;

        private void OnValidate()
        {
            Dictionary<Type, BaseMenuItemView> popupsDict = new ();

            foreach (var prefab in _prefabs)
            {
                if(!popupsDict.TryAdd(prefab.GetType(), prefab))
                    Debug.LogError($"Prefab for type {prefab.GetType()} already exists");
            }
        }
    }
}