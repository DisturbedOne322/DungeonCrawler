using Gameplay.Combat;
using Gameplay.Skills.Core;
using UnityEngine;

namespace Gameplay.Skills.OffensiveSkills
{
    [CreateAssetMenu(fileName = "BaseOffensiveSkill", menuName = "Gameplay/Skills/BaseOffensiveSkill")]
    public class BaseAttackSkill : OffensiveSkill
    {
        public override bool CanUse(CombatService combatService) => true;
    }
}