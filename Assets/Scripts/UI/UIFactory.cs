using System;
using AssetManagement.AssetProviders;
using Gameplay.Services;
using UI.Core;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIFactory : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        
        private ContainerFactory _factory;
        private PopupsRegistry _popupsRegistry;

        [Inject]
        private void Construct(ContainerFactory factory, PopupsRegistry popupsRegistry)
        {
            _factory = factory;
            _popupsRegistry = popupsRegistry;
        }

        public T CreatePopup<T>() where T : BasePopup
        {
            var prefab = _popupsRegistry.GetPopupPrefab<T>();
            
            var popup = _factory.Create<T>(prefab.gameObject);
            popup.transform.SetParent(_canvas.transform, false);
            return popup;
        }
    }
}