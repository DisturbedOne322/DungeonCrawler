using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Core
{
    public abstract class BasePopup : MonoBehaviour
    {
        [SerializeField] private GameObject _firstSelected;

        private void Awake()
        {
            if(_firstSelected)
                EventSystem.current.SetSelectedGameObject(_firstSelected);
            
            InitializePopup();
        }

        protected abstract void InitializePopup();

        public abstract void ShowPopup();
        public abstract void HidePopup();

        public void DestroyPopup() => Destroy(gameObject);
    }
}