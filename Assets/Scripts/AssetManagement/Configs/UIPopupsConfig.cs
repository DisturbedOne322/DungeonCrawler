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

        private readonly Dictionary<Type, BasePopup> _popupsDict = new();

        private void OnEnable()
        {
            foreach (var prefab in _prefabs)
                _popupsDict.Add(prefab.GetType(), prefab);
        }

        public bool TryGetPopup<T>(out T result) where T : BasePopup
        {
            if (_popupsDict.TryGetValue(typeof(T), out var popup))
            {
                result = popup as T;
                return true;
            }

            throw new Exception("Popup of type " + typeof(T).Name + " is not registered");
        }
    }
}