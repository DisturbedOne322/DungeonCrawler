using UnityEngine;

namespace Gameplay.Buffs.Core
{
    public abstract class BaseBuffData : BaseGameItem
    {
        [SerializeField] private BuffDurationData _buffDurationData;
        [SerializeField] private BuffTriggerType _triggerType;
        [SerializeField] private BuffReapplyType _buffReapplyType;
        [SerializeField, Min(1)] private int _maxStacks = 1;
        
        public BuffDurationData BuffDurationData => _buffDurationData;
        public BuffTriggerType TriggerType => _triggerType;
        public BuffReapplyType BuffReapplyType => _buffReapplyType;
        public int MaxStacks => _maxStacks;
    }
}