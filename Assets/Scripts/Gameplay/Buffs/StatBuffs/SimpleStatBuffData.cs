using Gameplay.Buffs.StatBuffsCore;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Buffs.StatBuffs
{
    [CreateAssetMenu(fileName = "SimpleStatBuff", menuName = "Gameplay/Buffs/StatBuffs/SimpleStatBuff")]
    public class SimpleStatBuffData : StatBuffData
    {
        [SerializeField] private int _increaseAmount;

        protected override int GetBuffDelta(IEntity unit) => _increaseAmount;
    }
}