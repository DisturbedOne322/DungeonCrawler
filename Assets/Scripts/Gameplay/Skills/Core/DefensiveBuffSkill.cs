using Gameplay.Buffs.DefensiveCore;
using UnityEngine;

namespace Gameplay.Skills.Core
{
    public abstract class DefensiveBuffSkill : BaseSkill
    {
        [SerializeField] protected DefensiveBuffData DefensiveBuffData;
    }
}