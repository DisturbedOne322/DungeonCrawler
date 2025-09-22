using System;
using Cysharp.Threading.Tasks;
using StateMachine.BattleMenu;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BattleMenu
{
    public class BaseBattleMenuItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _background;

        private IDisposable _disposable;
        
        public void Bind(BattleMenuItemData data)
        {
            _text.text = data.Label;

            _disposable = data.IsHighlighted
                .Subscribe(isOn => _background.color = isOn ? Color.green : Color.white);

            _background.color = data.IsSelectable() ? Color.white : Color.grey;
        }
        
        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}