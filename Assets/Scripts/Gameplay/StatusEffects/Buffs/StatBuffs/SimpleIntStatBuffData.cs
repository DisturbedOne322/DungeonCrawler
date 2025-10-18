using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.StatBuffs
{
    [CreateAssetMenu(fileName = "SimpleIntStatBuff", menuName = "Gameplay/Buffs/StatBuffs/SimpleIntStatBuffData")]
    public class SimpleIntStatBuffData : StatBuffData
    {
        [SerializeField] [Min(1)] private int _increaseAmount;

        protected override float GetBuffDelta() => _increaseAmount;
    }
}