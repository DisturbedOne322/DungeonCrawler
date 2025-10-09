using Gameplay.Combat;
using UnityEngine;

namespace Gameplay.Buffs.Core
{
    public abstract class BaseBuffData : BaseGameItem
    {
        [SerializeField] private BuffDurationData _buffDurationData;
        [SerializeField] private BuffTriggerType _triggerType;
        
        public BuffDurationData BuffDurationData => _buffDurationData;
        public BuffTriggerType TriggerType => _triggerType;
    }
}