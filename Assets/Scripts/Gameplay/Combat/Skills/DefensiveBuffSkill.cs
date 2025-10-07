using Cysharp.Threading.Tasks;
using Gameplay.Buffs;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    public abstract class DefensiveBuffSkill : BaseSkill
    {
        [SerializeField] protected DefensiveBuffData DefensiveBuffData;
    }
}