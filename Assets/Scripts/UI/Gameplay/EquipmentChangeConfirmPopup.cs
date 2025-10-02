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
        [SerializeField] private ItemDataView _oldEquipData;
        [SerializeField] private ItemDataView _newEquipData;

        [SerializeField] private Button _keepButton;
        [SerializeField] private Button _changeButton;
        
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
        }

        public override void HidePopup()
        {
        }
        
        public void SetData(BaseGameItem oldItem, BaseGameItem newItem)
        {
            _oldEquipData.SetData(oldItem);
            _newEquipData.SetData(newItem);
        }
    }
}