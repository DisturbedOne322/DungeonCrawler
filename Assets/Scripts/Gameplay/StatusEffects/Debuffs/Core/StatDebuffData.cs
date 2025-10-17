using Data;
using Gameplay.Facades;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.Debuffs
{
    public abstract class StatDebuffData : BaseStatusEffectData
    {
        [SerializeField] private StatType _debuffedStatType;

        public StatType DebuffedStatType => _debuffedStatType;

        public StatDebuffInstance CreateDebuffInstance(IEntity target)
        {
            var buffDelta = GetDebuffDelta(target);
            var instance = StatDebuffInstance.Create(this, buffDelta);
            return instance;
        }

        protected abstract float GetDebuffDelta(IEntity unit);
    }
}