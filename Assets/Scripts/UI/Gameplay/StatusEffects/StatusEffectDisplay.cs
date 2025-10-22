using Gameplay.StatusEffects.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Gameplay
{
    public class StatusEffectDisplay : MonoBehaviour
    {
        [SerializeField] private Image _statusEffectIcon;
        [SerializeField] private TextMeshProUGUI _stacksText;
        [SerializeField] private TextMeshProUGUI _turnsLeftText;
        
        public void SetData(BaseStatusEffectInstance instance)
        {
            SetIcon(instance);
            ShowStacks(instance);
            ShowDuration(instance);
        }

        private void SetIcon(BaseStatusEffectInstance instance)
        {
            _statusEffectIcon.sprite = instance.StatusEffectData.Icon;
        }

        private void ShowStacks(BaseStatusEffectInstance instance)
        {
            int stacks = instance.Stacks;
            bool showStacks = stacks > 1;
            
            _stacksText.gameObject.SetActive(showStacks);
            if(showStacks)
                _stacksText.text = "X" + stacks.ToString();
        }

        private void ShowDuration(BaseStatusEffectInstance instance)
        {
            var durationType = instance.EffectExpirationType;

            bool showDuration = durationType == StatusEffectExpirationType.TurnCount;
            _turnsLeftText.gameObject.SetActive(showDuration);

            if (showDuration)
            {
                int turnsLeft = instance.TurnDurationLeft;
                
                if(turnsLeft == 1)
                    _turnsLeftText.text = instance.TurnDurationLeft + " turn.";
                else
                    _turnsLeftText.text = instance.TurnDurationLeft + " turns.";
            }
        }
    }
}