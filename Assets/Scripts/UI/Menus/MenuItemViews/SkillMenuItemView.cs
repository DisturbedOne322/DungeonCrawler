using TMPro;
using UnityEngine;

namespace UI.Menus.MenuItemViews
{
    public class SkillMenuItemView : BaseMenuItemView
    {
        [SerializeField] private TextMeshProUGUI _descText;

        public void SetDescription(string desc)
        {
            _descText.text = desc;
        }
    }
}