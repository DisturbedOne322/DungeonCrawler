using Data;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.Debuffs.Core
{
    public abstract class StatDebuffData : BaseStatusEffectData
    {
        [SerializeField] private StatType _debuffedStatType;

        public StatType DebuffedStatType => _debuffedStatType;

        public override BaseStatusEffectInstance CreateInstance()
        {
            var buffDelta = GetDebuffDelta();
            var instance = StatDebuffInstance.Create(this, buffDelta);
            return instance;
        }

        protected abstract float GetDebuffDelta();
    }
}