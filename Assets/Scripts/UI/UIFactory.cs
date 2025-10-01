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
        
        private UIPrefabsProvider _uiPrefabsProvider;
        private ContainerFactory _factory;

        [Inject]
        private void Construct(ContainerFactory factory, UIPrefabsProvider uiPrefabsProvider)
        {
            _factory = factory;
            _uiPrefabsProvider = uiPrefabsProvider;
        }

        public T CreatePopup<T>() where T : BasePopup
        {
            var prefab = _uiPrefabsProvider.GetAsset<T>();
            var popup = _factory.Create<T>(prefab.gameObject);
            popup.transform.SetParent(_canvas.transform, false);
            return popup;
        }
    }
}