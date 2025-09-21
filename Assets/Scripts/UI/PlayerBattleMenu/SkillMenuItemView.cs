using TMPro;
using UnityEngine;

namespace UI.PlayerBattleMenu
{
    public class SkillMenuItemView : MenuItemView
    {
        [SerializeField] private TextMeshProUGUI _description;
        
        public void SetDescription(string description) => _description.text = description;
    }
}