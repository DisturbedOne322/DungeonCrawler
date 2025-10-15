using Gameplay.Buffs.StatBuffsCore;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Buffs.StatBuffs
{
    [CreateAssetMenu(fileName = "SimpleFloatStatBuff", menuName = "Gameplay/Buffs/StatBuffs/SimpleFloatStatBuffData")]
    public class SimpleFloatStatBuffData : StatBuffData
    {
        [SerializeField, Min(0f)] private float _increaseAmount;

        protected override float GetBuffDelta(IEntity unit) => _increaseAmount;
    }
}