using TMPro;
using UnityEngine;

namespace UI.BattleMenu
{
    public class ItemMenuItemView : BaseMenuItemView
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