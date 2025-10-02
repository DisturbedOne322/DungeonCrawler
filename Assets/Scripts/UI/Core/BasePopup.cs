using Cysharp.Threading.Tasks;
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

        public virtual async UniTask ShowPopup() => await _popupAnimator.PlayShowAnim();
        public virtual async UniTask HidePopup() => await _popupAnimator.PlayHideAnim(DestroyPopup);

        public void DestroyPopup() => Destroy(gameObject);
        
        protected virtual void InitializePopup(){}
        protected void SetSelectedGameObject(GameObject gameObject) => EventSystem.current.SetSelectedGameObject(_firstSelected);
    }
}