using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Notifications
{
    public class ConsumableNotificationDisplay : BaseNotificationDisplay
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descText;
        [SerializeField] private TextMeshProUGUI _amountText;
        
        public void SetData(BaseGameItem item, int amount)
        {
            _image.sprite = item.Icon;
            _nameText.text = item.Name;
            _descText.text = item.Description;
            _amountText.text = "+" + amount;
        }
    }
}