using System;
using Gameplay;
using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Gameplay.Experience
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
            _keepButton.onClick.AddListener(() => OnKeepPressed?.Invoke());

            _changeButton.onClick.AddListener(() => OnChangePressed?.Invoke());
        }

        public void SetData(BaseGameItem oldItem, BaseGameItem newItem)
        {
            _oldEquipData.SetData(oldItem);
            _newEquipData.SetData(newItem);
        }
    }
}