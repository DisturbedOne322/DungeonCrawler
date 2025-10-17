using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.StatBuffs
{
    [CreateAssetMenu(fileName = "SimpleFloatStatBuff", menuName = "Gameplay/Buffs/StatBuffs/SimpleFloatStatBuffData")]
    public class SimpleFloatStatBuffData : StatBuffData
    {
        [SerializeField] [Min(0f)] private float _increaseAmount;

        protected override float GetBuffDelta(IEntity unit)
        {
            return _increaseAmount;
        }
    }
}