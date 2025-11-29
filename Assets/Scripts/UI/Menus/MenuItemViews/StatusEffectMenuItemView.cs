using TMPro;
using UI.BattleMenu;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menus.MenuItemViews
{
    public class StatusEffectMenuItemView : BaseMenuItemView
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _descText;

        public void SetDescription(string desc) => _descText.text = desc;

        public void SetIcon(Sprite icon) => _iconImage.sprite = icon;
    }
}