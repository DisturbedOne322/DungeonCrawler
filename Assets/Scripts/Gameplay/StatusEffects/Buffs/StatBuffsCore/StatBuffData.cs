using Data;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.StatBuffsCore
{
    public abstract class StatBuffData : BaseStatusEffectData
    {
        [SerializeField] private StatType _buffedStatType;

        public StatType BuffedStatType => _buffedStatType;

        protected abstract float GetBuffDelta();

        public override BaseStatusEffectInstance CreateInstance()
        {
            var buffDelta = GetBuffDelta();
            var instance = StatBuffInstance.Create(this, buffDelta);
            return instance;
        }
    }
}