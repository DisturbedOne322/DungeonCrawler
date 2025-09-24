using TMPro;
using UnityEngine;

namespace UI.BattleUI.Damage
{
    public class NumberObjectView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _numberText;
        
        public TextMeshProUGUI NumberText => _numberText;
        
        public void SetText(int amount, char prefix)
        {
            _numberText.text = prefix + amount.ToString();
        }
    }
}