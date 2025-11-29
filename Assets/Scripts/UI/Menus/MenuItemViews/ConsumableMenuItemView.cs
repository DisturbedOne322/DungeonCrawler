using TMPro;
using UnityEngine;

namespace UI.Menus.MenuItemViews
{
    public class ConsumableMenuItemView : BaseMenuItemView
    {
        [SerializeField] private TextMeshProUGUI _descText;
        [SerializeField] private TextMeshProUGUI _quantityText;

        public void SetDescription(string desc)
        {
            _descText.text = desc;
        }

        public void SetQuantity(int quantity)
        {
            _quantityText.text = "x" + quantity;
        }
    }
}