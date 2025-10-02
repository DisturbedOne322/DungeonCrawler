using System;
using AssetManagement.AssetProviders;
using AssetManagement.AssetProviders.Core;
using AssetManagement.Configs;
using Gameplay.Services;
using UI.Core;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIFactory : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        
        private BaseConfigProvider<UIPopupsConfig> _uiPopupsConfigProvider;
        private ContainerFactory _factory;

        [Inject]
        private void Construct(ContainerFactory factory, BaseConfigProvider<UIPopupsConfig> uiPopupsConfigProvider)
        {
            _factory = factory;
            _uiPopupsConfigProvider = uiPopupsConfigProvider;
        }

        public T CreatePopup<T>() where T : BasePopup
        {
            var config = _uiPopupsConfigProvider.GetConfig();
            
            if (!config.TryGetPopup<T>(out var prefab))
                throw new Exception($"Could not find popup {typeof(T)}");
            
            var popup = _factory.Create<T>(prefab.gameObject);
            popup.transform.SetParent(_canvas.transform, false);
            return popup;
        }
    }
}