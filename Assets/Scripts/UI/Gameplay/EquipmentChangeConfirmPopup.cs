using System;
using DG.Tweening;
using Gameplay.Combat;
using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Gameplay
{
    public class EquipmentChangeConfirmPopup : BasePopup
    {
        [SerializeField] private CanvasGroup _canvas;
        
        [SerializeField] private ItemDataView _oldEquipData;
        [SerializeField] private ItemDataView _newEquipData;

        [SerializeField] private Button _keepButton;
        [SerializeField] private Button _changeButton;
        
        [SerializeField] private float _showTime = 0.5f;
        [SerializeField] private float _hideTime = 0.5f;

        public event Action OnKeepPressed;
        public event Action OnChangePressed;

        protected override void InitializePopup()
        {
            _keepButton.onClick.AddListener(() =>
            {
                OnKeepPressed?.Invoke();
                HidePopup();
            });
            
            _changeButton.onClick.AddListener(() =>
            {
                OnChangePressed?.Invoke();
                HidePopup();
            });
        }

        public override void ShowPopup()
        {
            _canvas.DOFade(1, _showTime).SetEase(Ease.OutBack).SetLink(gameObject);
        }

        public override void HidePopup()
        {
            _canvas.DOKill();
            _canvas.DOFade(0, _hideTime).
                SetEase(Ease.OutBack).
                SetLink(gameObject).
                OnComplete(DestroyPopup);
        }
        
        public void SetData(BaseGameItem oldItem, BaseGameItem newItem)
        {
            _oldEquipData.SetData(oldItem);
            _newEquipData.SetData(newItem);
        }
    }
}