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

        private IDisposable _disposable;

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        public void Bind(MenuItemData data)
        {
            _text.text = data.Label;

            _disposable = data.IsHighlighted
                .Subscribe(isOn =>
                {
                    if (!data.IsSelectable())
                        SetLocked();
                    else
                        SetSelectionColor(isOn);
                });
        }

        private void SetLocked()
        {
            _background.color = Color.grey;
        }

        private void SetSelectionColor(bool isOn)
        {
            _background.color = isOn ? Color.green : Color.white;
        }
    }
}