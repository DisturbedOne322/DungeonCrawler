using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BattleMenu
{
    public class BaseMenuItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _background;

        private CompositeDisposable _disposables;

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        public void Bind(MenuItemData data)
        {
            _text.text = data.Label;
            _disposables = new();
            
            data.IsHighlighted
                .Subscribe(isHighlighted =>
                {
                    SetSelectionColor(isHighlighted, data.IsSelectable.Value);
                }).AddTo(_disposables);
            
            data.IsSelectable
                .Subscribe(isSelectable =>
                {
                    SetSelectionColor(data.IsHighlighted.Value, isSelectable);
                }).AddTo(_disposables);
        }

        private void SetSelectionColor(bool isHighlighted, bool canBeSelected)
        {
            if (!canBeSelected)
            {
                SetLocked();
                return;
            }
            
            _background.color = isHighlighted ? Color.green : Color.white;
        }

        private void SetLocked()
        {
            _background.color = Color.grey;
        }
    }
}