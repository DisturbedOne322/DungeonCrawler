using System;
using System.Collections.Generic;
using UI.Core;
using UnityEngine;

namespace AssetManagement.Configs
{
    [CreateAssetMenu(fileName = "UIPopupsConfig", menuName = "Visuals/UI/PopupsConfig")]
    public class UIPopupsConfig : BaseConfig
    {
        [SerializeField] private List<BasePopup> _prefabs;
        
        public IReadOnlyList<BasePopup> Prefabs => _prefabs;

        private void OnValidate()
        {
            Dictionary<Type, BasePopup> popupsDict = new ();

            foreach (var prefab in _prefabs)
            {
                if(!popupsDict.TryAdd(prefab.GetType(), prefab))
                    Debug.LogError($"Prefab for type {prefab.GetType()} already exists");
            }
        }
    }
}