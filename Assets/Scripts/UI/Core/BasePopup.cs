using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Core
{
    public abstract class BasePopup : MonoBehaviour
    {
        [SerializeField] private GameObject _firstSelected;
        [SerializeField] private PopupAnimator _popupAnimator;
        
        private void Awake()
        {
            if (_firstSelected)
                SetSelectedGameObject(_firstSelected);

            InitializePopup();
        }

        public virtual void ShowPopup() => _popupAnimator.PlayShowAnim();
        public virtual void HidePopup() => _popupAnimator.PlayHideAnim(DestroyPopup);

        public void DestroyPopup() => Destroy(gameObject);
        
        protected virtual void InitializePopup(){}
        protected void SetSelectedGameObject(GameObject gameObject) => EventSystem.current.SetSelectedGameObject(_firstSelected);
    }
}