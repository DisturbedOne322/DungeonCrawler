using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Core
{
    public class UIPrefabsContainer : MonoBehaviour
    {
        [SerializeField] private List<BasePopup> _prefabs;

        private readonly Dictionary<Type, BasePopup> _popups = new();

        private void Awake()
        {
            foreach (var prefab in _prefabs) 
                _popups.TryAdd(prefab.GetType(), prefab);
        }

        public T GetPopup<T>() where T : BasePopup
        {
            if (_popups.TryGetValue(typeof(T), out var popup))
                return popup as T;

            Debug.LogError($"Popup of type {typeof(T).Name} not found!");
            return null;
        }
    }
}