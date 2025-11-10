using UnityEngine;

namespace Gameplay.StatusEffects.Core
{
    public abstract class BaseStatusEffectData : BaseGameItem
    {
        [SerializeField] private StatusEffectDurationData _statusEffectDurationData;
        [SerializeField] private StatusEffectTriggerType _triggerType;
        [SerializeField] private StatusEffectReapplyType _statusEffectReapplyType;
        [SerializeField] [Min(1)] private int _maxStacks = 1;

        public StatusEffectDurationData StatusEffectDurationData => _statusEffectDurationData;
        public StatusEffectTriggerType TriggerType => _triggerType;
        public StatusEffectReapplyType StatusEffectReapplyType => _statusEffectReapplyType;
        public int MaxStacks => _maxStacks;

        public abstract BaseStatusEffectInstance CreateInstance();
    }
}