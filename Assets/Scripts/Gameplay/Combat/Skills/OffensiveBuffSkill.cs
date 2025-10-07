using Gameplay.Buffs;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    public abstract class OffensiveBuffSkill : BaseSkill
    {
        [SerializeField] protected OffensiveBuffData OffensiveBuffData;
    }
}