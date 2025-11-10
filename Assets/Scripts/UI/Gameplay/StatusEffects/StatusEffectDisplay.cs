using System;
using Gameplay.StatusEffects.Core;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Gameplay.StatusEffects
{
    public class StatusEffectDisplay : MonoBehaviour
    {
        [SerializeField] private Image _statusEffectIcon;
        [SerializeField] private TextMeshProUGUI _stacksText;
        [SerializeField] private TextMeshProUGUI _turnsLeftText;
        [SerializeField] private TextMeshProUGUI _effectNameText;

        private IDisposable _stacksSubscription;
        private IDisposable _turnsLeftSubscription;

        private void OnDestroy()
        {
            Dispose();
        }

        public void SetData(BaseStatusEffectInstance instance)
        {
            _effectNameText.text = instance.StatusEffectData.Name;
            Dispose();
            SetIcon(instance);
            InitStacksDisplay(instance);
            InitDurationDisplay(instance);
        }

        private void SetIcon(BaseStatusEffectInstance instance)
        {
            _statusEffectIcon.sprite = instance.StatusEffectData.Icon;
        }

        private void InitStacksDisplay(BaseStatusEffectInstance instance)
        {
            if (ShouldShowStacks(instance))
            {
                _stacksText.gameObject.SetActive(true);
                _stacksSubscription = instance.Stacks.Subscribe(UpdateStacks);
            }
            else
            {
                _stacksText.gameObject.SetActive(false);
            }
        }

        private void InitDurationDisplay(BaseStatusEffectInstance instance)
        {
            if (ShouldShowDuration(instance))
            {
                _turnsLeftText.gameObject.SetActive(true);
                _turnsLeftSubscription = instance.TurnDurationLeft.Subscribe(UpdateDuration);
            }
            else
            {
                _turnsLeftText.gameObject.SetActive(false);
            }
        }

        private void UpdateStacks(int stacks)
        {
            _stacksText.text = "X" + stacks;
        }

        private void UpdateDuration(int duration)
        {
            if (duration == 1)
                _turnsLeftText.text = duration + " turn.";
            else
                _turnsLeftText.text = duration + " turns.";
        }

        private bool ShouldShowStacks(BaseStatusEffectInstance instance)
        {
            return instance.StatusEffectData.MaxStacks > 1;
        }

        private bool ShouldShowDuration(BaseStatusEffectInstance instance)
        {
            return instance.EffectExpirationType == StatusEffectExpirationType.TurnCount;
        }

        private void Dispose()
        {
            _stacksSubscription?.Dispose();
            _turnsLeftSubscription?.Dispose();
        }
    }
}