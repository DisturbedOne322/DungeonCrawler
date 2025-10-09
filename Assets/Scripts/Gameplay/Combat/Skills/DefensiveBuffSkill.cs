using Cysharp.Threading.Tasks;
using Gameplay.Buffs;
using Gameplay.Buffs.DefensiveCore;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    public abstract class DefensiveBuffSkill : BaseSkill
    {
        [SerializeField] protected DefensiveBuffData DefensiveBuffData;
    }
}