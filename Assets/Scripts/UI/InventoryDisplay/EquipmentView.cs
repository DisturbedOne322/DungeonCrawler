using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InventoryDisplay
{
    public class EquipmentView : MonoBehaviour
    {
        [SerializeField] private GameObject _equippedParent;
        [SerializeField] private GameObject _unequippedParent;
        
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private TextMeshProUGUI _itemDescription;

        public void SetEquipped(BaseGameItem item)
        {
            _equippedParent.SetActive(true);
            _itemIcon.sprite = item.Icon;
            _itemName.text = item.Name;
            _itemDescription.text = item.Description;
        }

        public void SetUnequipped()
        {
            _unequippedParent.SetActive(true);
        }
    }
}