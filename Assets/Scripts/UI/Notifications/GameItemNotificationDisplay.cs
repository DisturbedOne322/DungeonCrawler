using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Notifications
{
    public class GameItemNotificationDisplay : BaseNotificationDisplay
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descText;

        public void SetData(BaseGameItem item)
        {
            _image.sprite = item.Icon;
            _nameText.text = item.Name;
            _descText.text = item.Description;
        }
    }
}