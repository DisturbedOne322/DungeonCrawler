using TMPro;
using UnityEngine;

namespace UI.BattleMenu
{
    public class SkillBattleMenuItemView : BaseBattleMenuItemView
    {
        [SerializeField] private TextMeshProUGUI _descText;
        
        public void SetDescription(string desc) => _descText.text = desc;
    }
}