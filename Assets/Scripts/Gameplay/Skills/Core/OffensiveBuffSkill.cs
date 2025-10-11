using Gameplay.Buffs.OffensiveCore;
using UnityEngine;

namespace Gameplay.Skills.Core
{
    public abstract class OffensiveBuffSkill : BaseSkill
    {
        [SerializeField] protected OffensiveBuffData OffensiveBuffData;
    }
}