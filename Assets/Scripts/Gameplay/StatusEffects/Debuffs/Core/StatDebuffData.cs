using Data;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.Debuffs.Core
{
    public abstract class StatDebuffData : BaseStatusEffectData
    {
        [SerializeField] private StatType _debuffedStatType = StatType.Constitution;
        [SerializeField] private DebuffTarget _debuffTarget = DebuffTarget.OtherUnit;

        public StatType DebuffedStatType => _debuffedStatType;
        public DebuffTarget DebuffTarget => _debuffTarget;

        public override BaseStatusEffectInstance CreateInstance()
        {
            var buffDelta = GetDebuffDelta();
            var instance = StatDebuffInstance.Create(this, buffDelta);
            return instance;
        }

        protected abstract float GetDebuffDelta();
    }
}