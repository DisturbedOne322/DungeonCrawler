using Data;
using Gameplay.Facades;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.StatBuffsCore
{
    public abstract class StatBuffData : BaseStatusEffectData
    {
        [SerializeField] private StatType _buffedStatType;

        public StatType BuffedStatType => _buffedStatType;

        public StatBuffInstance CreateBuffInstance(IEntity unit)
        {
            var buffDelta = GetBuffDelta(unit);
            var instance = StatBuffInstance.Create(this, buffDelta);
            return instance;
        }

        protected abstract float GetBuffDelta(IEntity unit);
    }
}