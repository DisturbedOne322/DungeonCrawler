using Data;
using Gameplay.Buffs.Core;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Buffs.StatBuffsCore
{
    public abstract class StatBuffData : BaseBuffData
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