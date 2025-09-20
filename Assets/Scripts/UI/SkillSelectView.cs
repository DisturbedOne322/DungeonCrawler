using Gameplay.Combat.Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SkillSelectView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descText;
        [SerializeField] private Image _backgroundImage;

        public void SetData(BaseSkill skill)
        {
            _nameText.text = skill.Name;
            _descText.text = skill.Description;
        }
        
        public void SetSelected(bool selected) => _backgroundImage.color = selected ? Color.green : Color.white;
        public void SetUnusable() => _backgroundImage.color = Color.grey;
    }
}