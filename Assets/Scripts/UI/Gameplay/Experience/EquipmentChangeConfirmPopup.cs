using System;
using Attributes;
using Data;
using Gameplay;
using Gameplay.Player;
using UI.Core;
using UI.Navigation;
using UnityEngine;
using Zenject;

namespace UI.Gameplay.Experience
{
    public class EquipmentChangeConfirmPopup : BasePopup
    {
        [SerializeField] private ItemDataView _oldEquipData;
        [SerializeField] private ItemDataView _newEquipData;

        [SerializeField] [Separator] [Space] private MenuItemDataMono _keepMenuItem;
        [SerializeField] private MenuItemDataMono _changeMenuItem;

        public event Action OnKeepPressed;
        public event Action OnChangePressed;

        [Inject]
        private void Construct(HorizontalUINavigator uiNavigator, PlayerInputProvider playerInputProvider,
            GameplayData gameplayData)
        {
            uiNavigator.BindToObservable(OnCloseCalled);

            uiNavigator.AddMenuItem(_keepMenuItem, () => OnKeepPressed?.Invoke());
            uiNavigator.AddMenuItem(_changeMenuItem, () => OnChangePressed?.Invoke());
        }

        public void SetData(BaseGameItem oldItem, BaseGameItem newItem)
        {
            _oldEquipData.SetData(oldItem);
            _newEquipData.SetData(newItem);
        }
    }
}