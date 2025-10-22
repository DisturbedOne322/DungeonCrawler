using System;
using Gameplay.StatusEffects.Core;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Gameplay
{
    public class StatusEffectDisplay : MonoBehaviour
    {
        [SerializeField] private Image _statusEffectIcon;
        [SerializeField] private TextMeshProUGUI _stacksText;
        [SerializeField] private TextMeshProUGUI _turnsLeftText;
        
        private IDisposable _stacksSubscription;
        private IDisposable _turnsLeftSubscription;

        private void OnDestroy() => Dispose();

        public void SetData(BaseStatusEffectInstance instance)
        {
            Dispose();
            SetIcon(instance);
            InitStacksDisplay(instance);
            InitDurationDisplay(instance);
        }

        private void SetIcon(BaseStatusEffectInstance instance) => _statusEffectIcon.sprite = instance.StatusEffectData.Icon;

        private void InitStacksDisplay(BaseStatusEffectInstance instance)
        {
            if (ShouldShowStacks(instance))
            {
                _stacksText.gameObject.SetActive(true);
                _stacksSubscription = instance.Stacks.Subscribe(_ => UpdateStacks(instance));
            }
            else
                _stacksText.gameObject.SetActive(false);
        }

        private void InitDurationDisplay(BaseStatusEffectInstance instance)
        {
            if (ShouldShowDuration(instance))
            {
                _turnsLeftText.gameObject.SetActive(true);
                _turnsLeftSubscription = instance.TurnDurationLeft.Subscribe(_ => UpdateDuration(instance));
            }
            else
                _turnsLeftText.gameObject.SetActive(false);
        }

        private void UpdateStacks(BaseStatusEffectInstance instance)
        {
            int stacks = instance.Stacks.Value;
            _stacksText.text = "X" + stacks;
        }

        private void UpdateDuration(BaseStatusEffectInstance instance)
        {
            int turnsLeft = instance.TurnDurationLeft.Value;
            
            if(turnsLeft == 1)
                _turnsLeftText.text = instance.TurnDurationLeft.Value + " turn.";
            else
                _turnsLeftText.text = instance.TurnDurationLeft.Value + " turns.";
        }

        private bool ShouldShowStacks(BaseStatusEffectInstance instance) => 
            instance.StatusEffectData.MaxStacks > 1;
        
        private bool ShouldShowDuration(BaseStatusEffectInstance instance) => 
            instance.EffectExpirationType == StatusEffectExpirationType.TurnCount;

        private void Dispose()
        {
            _stacksSubscription?.Dispose();
            _turnsLeftSubscription?.Dispose();
        }
    }
}