using TMPro;
using UnityEngine;

namespace UI.BattleUI.Damage
{
    public class DamageNumberView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _damageText;
        
        public TextMeshProUGUI DamageText => _damageText;
        
        public void SetText(int damage)
        {
            _damageText.text = damage.ToString();
        }
    }
}