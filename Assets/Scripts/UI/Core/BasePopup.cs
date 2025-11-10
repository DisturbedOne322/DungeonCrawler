using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Core
{
    public abstract class BasePopup : MonoBehaviour
    {
        [SerializeField] private PopupAnimator _popupAnimator;

        private void Awake()
        {
            InitializePopup();
        }

        public virtual async UniTask ShowPopup()
        {
            await _popupAnimator.PlayShowAnim();
        }

        public virtual async UniTask HidePopup()
        {
            await _popupAnimator.PlayHideAnim(DestroyPopup);
        }

        public void DestroyPopup()
        {
            Destroy(gameObject);
        }

        protected virtual void InitializePopup()
        {
        }
    }
}