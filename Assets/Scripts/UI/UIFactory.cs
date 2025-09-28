using Gameplay.Services;
using UI.Core;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIFactory : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private UIPrefabsContainer _uiPrefabsContainer;
        
        private ContainerFactory _factory;

        [Inject]
        private void Construct(ContainerFactory factory)
        {
            _factory = factory;
        }

        public T CreatePopup<T>() where T : BasePopup
        {
            var prefab = _uiPrefabsContainer.GetPopup<T>();
            var popup = _factory.Create<T>(prefab.gameObject);
            popup.transform.SetParent(_canvas.transform, false);
            return popup;
        }
    }
}