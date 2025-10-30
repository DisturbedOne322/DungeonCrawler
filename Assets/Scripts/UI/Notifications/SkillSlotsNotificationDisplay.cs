using TMPro;
using UnityEngine;

namespace UI.Notifications
{
    public class SkillSlotsNotificationDisplay : BaseNotificationDisplay
    {
        [SerializeField] private TextMeshProUGUI _notificationText;
        
        public void SetData(int amount)
        {
            _notificationText.text = "NEW SKILL SLOTS: " + amount;
        }
    }
}