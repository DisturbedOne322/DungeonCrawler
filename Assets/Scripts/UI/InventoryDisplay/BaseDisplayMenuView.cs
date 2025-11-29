using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InventoryDisplay
{
    public abstract class BaseDisplayMenuView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _image;
        
        public CanvasGroup CanvasGroup => _canvasGroup;
        public Image Image => _image;
        
        public event Action OnBack;
        
        private void Start()
        {
            Initialize();
        }

        protected abstract void Initialize();

        public virtual void Select()
        {
            
        }

        public virtual void Deselect()
        {
            
        }
        
        protected void InvokeOnBack() => OnBack?.Invoke();
    }
}