using TMPro;
using UnityEngine;

namespace UI.BattleMenu
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