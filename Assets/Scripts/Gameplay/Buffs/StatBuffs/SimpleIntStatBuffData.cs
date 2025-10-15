using Gameplay.Buffs.StatBuffsCore;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Buffs.StatBuffs
{
    [CreateAssetMenu(fileName = "SimpleIntStatBuff", menuName = "Gameplay/Buffs/StatBuffs/SimpleIntStatBuffData")]
    public class SimpleIntStatBuffData : StatBuffData
    {
        [SerializeField, Min(1)] private int _increaseAmount;

        protected override float GetBuffDelta(IEntity unit) => _increaseAmount;
    }
}