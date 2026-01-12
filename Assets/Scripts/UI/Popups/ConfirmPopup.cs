using System;
using Attributes;
using Data.UI;
using Gameplay.Player;
using TMPro;
using UI.Core;
using UI.Navigation;
using UnityEngine;
using Zenject;

namespace UI.Popups
{
    public class ConfirmPopup : BasePopup
    {
        [SerializeField] private TextMeshProUGUI _messageText;

        [SerializeField] [Separator] [Space] private MenuItemDataMono _noItem;
        [SerializeField] private MenuItemDataMono _yesItem;

        public event Action<ConfirmChoice> OnDecisionMade;

        [Inject]
        private void Construct(HorizontalUINavigator uiNavigator, PlayerInputProvider playerInputProvider)
        {
            uiNavigator.BindToObservable(OnCloseCalled);
            uiNavigator.AddMenuItem(_noItem, () => OnDecisionMade?.Invoke(ConfirmChoice.Deny));
            uiNavigator.AddMenuItem(_yesItem, () => OnDecisionMade?.Invoke(ConfirmChoice.Accept));
        }

        public void SetMessage(string message)
        {
            _messageText.text = message;
        }
    }
}