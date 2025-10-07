using Gameplay.Combat;
using UnityEngine;

namespace Gameplay.Buffs
{
    public abstract class BaseBuffData : BaseGameItem
    {
        [SerializeField] private BuffDurationData _buffDurationData;
        [SerializeField] private BuffTriggerType _triggerType;
        
        public BuffDurationData BuffDurationData => _buffDurationData;
        public BuffTriggerType TriggerType => _triggerType;
    }
}